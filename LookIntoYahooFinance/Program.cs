using BJK.FinanceApi.Interfaces;
using BJK.FinanceApi.Classes;

// TEST from console app, get AAPL, NVDA, and TSLA
IEnumerable<string> TICKERS = ["AAPL", "NVDA", "TSLA"];

IDataExtractor yahooExtractor = new YahooQuotesApiCaller(TICKERS);

Console.WriteLine("Calling API");

// Call data extractor
await yahooExtractor.GetInformation();

Console.WriteLine("Call Complete");
Console.WriteLine("");

foreach (IFinanceInstrument financeInstrument in yahooExtractor.InstrumentsInformation)
{
    Console.WriteLine($"Stock Info For: {financeInstrument.Symbol}");
    Console.WriteLine($"Name: {financeInstrument.Name}");
    Console.WriteLine($"Rating: {financeInstrument.AnalystRating}");
    Console.WriteLine("");
}