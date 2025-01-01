using BJK.FinanceApi.Classes;
using BJK.FinanceApi.Interfaces;
using BJK.TickerExtract.Classes;
using BJK.TickerExtract.Interfaces;

// Load configurations
IReaderConfig config = GetReaderData.Configuration;
IManualTickerConfig manualTickerConfig = GetManualTickerData.Configuration;

// Might as well just check to see if we even have a file to write to
if (string.IsNullOrEmpty(config.FileToWriteTo))
{
    Console.WriteLine("Configuration does not have a place to write save data to");
    return;
}

// Assuming the above code did not run and we have a destination file to write to, run the rest of the program
// ExampleTickerCollector
// TickerCollector
ITickerCollector tickerCollector = new ExampleTickerCollector();
tickerCollector.Read(config);

// Random message to know the program has started
Console.WriteLine($"Processing {tickerCollector.Tickers.Count().ToString("N0")} tickers");

// Call the API
IDataExtractor yahooExtractor = new YahooQuotesApiCaller(tickerCollector.Tickers);

// Call data extractor
await yahooExtractor.GetInformation();

// Begin Write Output
using StreamWriter writer = new(config.FileToWriteTo, false);

// Add Tickers Not Found In The Normal List And We Want To Manually Add
foreach (string manualAdd in manualTickerConfig.ToAdd)
{
    writer.WriteLine($"{manualAdd},N/A");
}

// Write The Normal File
foreach (IFinanceInstrument financeInstrument in yahooExtractor.InstrumentsInformation)
{
    if (!manualTickerConfig.ToOmit.Contains(financeInstrument.Symbol))
    {
        writer.WriteLine($"{financeInstrument.Symbol},{financeInstrument.AnalystRating}");
    }
}

Console.WriteLine("All tickers have been processed, check the CSV file");