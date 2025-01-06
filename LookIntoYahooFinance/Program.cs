using BJK.Finance.DecisionMaking.Classes;
using BJK.Finance.DecisionMaking.Interfaces;
using BJK.FinanceApi.Classes;
using BJK.FinanceApi.Interfaces;
using BJK.TickerExtract.Classes;
using BJK.TickerExtract.Interfaces;

// Load configurations
IReaderConfig config = GetReaderData.Configuration;
IManualTickerConfig manualTickerConfig = GetManualTickerData.Configuration;
IPersonalData personalDataConfig = GetPersonalData.Configuration;

// Might as well just check to see if we even have a file to write to
if (string.IsNullOrEmpty(config.FileToWriteTo))
{
    Console.WriteLine("Configuration does not have a place to write save data to");
    return;
}

// Assuming the above code did not run and we have a destination file to write to, run the rest of the program
CboeWeeklyCsvDownloader tickerCollector = new();
await tickerCollector.ReadAsync(config);

// Call the API
List<string> allTickers = tickerCollector.Tickers.Concat(manualTickerConfig.ToAdd).ToList();
foreach (string toOmit in manualTickerConfig.ToOmit)
{
    allTickers.Remove(toOmit);
}

// Random message to know the program has started
Console.WriteLine($"Processing {allTickers.Count.ToString("N0")} tickers");
YahooQuotesApiCaller yahooExtractor = new(allTickers);

// Call data extractor
await yahooExtractor.GetInformation();

// Begin Write Output
using StreamWriter normalFileWriter = new(config.FileToWriteTo, false);

// Write The Normal File
foreach (IFinanceInstrument financeInstrument in yahooExtractor.InstrumentsInformation)
{
    normalFileWriter.WriteLine($"{financeInstrument.Symbol},{financeInstrument.AnalystRating}");
}

Console.WriteLine("Normal CSV File With Tickers and Analysts Ratings Ready");
normalFileWriter.Close();
await normalFileWriter.DisposeAsync();

Console.WriteLine("Begin writing the next moves file");

// Start looking into the decision maker
DecisionMaker decisionMaker = new(personalDataConfig, yahooExtractor.InstrumentsInformation);
decisionMaker.BuildStrategies();

// Start writing to next moves possible file
using StreamWriter nextMovesFiles = new(config.NextMovesFile, false);
nextMovesFiles.WriteLine("Symbol,Security Name,Rating,Strategy,Instruments Can Afford");

foreach (IOptionStrategyPossibility nextMove in decisionMaker.PossibleOptionsStrategies)
{
    nextMovesFiles.WriteLine($"{nextMove.FinanceInstrument.Symbol},\"{nextMove.FinanceInstrument.Name}\",{nextMove.FinanceInstrument.AnalystRating},{nextMove.Strategy},{nextMove.ContractsCanAfford}");
}

Console.WriteLine("Next Moves File Done");