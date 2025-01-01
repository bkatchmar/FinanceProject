using BJK.FinanceApi.Classes;
using BJK.FinanceApi.Interfaces;
using BJK.TickerExtract.Classes;
using BJK.TickerExtract.Interfaces;

IReaderConfig config = GetReaderData.Configuration;

// Might as well just check to see if we even have a file to write to
if (string.IsNullOrEmpty(config.FileToWriteTo))
{
    Console.WriteLine("Configuration does not have a place to write save data to");
    return;
}

// Assuming the above code did not run and we have a destination file to write to, run the rest of the program
ITickerCollector tickerCollector = new TickerCollector();
tickerCollector.Read(config);

Console.WriteLine($"Processing {tickerCollector.Tickers.Count().ToString("N0")} tickers");

IDataExtractor yahooExtractor = new YahooQuotesApiCaller(tickerCollector.Tickers);

// Call data extractor
await yahooExtractor.GetInformation();

using StreamWriter writer = new(config.FileToWriteTo, false);

foreach (IFinanceInstrument financeInstrument in yahooExtractor.InstrumentsInformation)
{
    writer.WriteLine($"{financeInstrument.Symbol},{financeInstrument.AnalystRating}");
}

Console.WriteLine("All tickers have been processed, check the CSV file");