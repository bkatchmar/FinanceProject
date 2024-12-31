using BJK.TickerExtract.Classes;
using BJK.TickerExtract.Interfaces;

IReaderConfig config = GetReaderData.Configuration;

ITickerCollector tickerCollector = new TickerCollector();
tickerCollector.Read(config);

Console.WriteLine(tickerCollector.Tickers.Count());