using YahooQuotesApi;

IEnumerable<string> TICKERS = new List<string>() { "AAPL", "TSLA", "NVDA" };

YahooQuotes yahooQuotes = new YahooQuotesBuilder().Build();

Dictionary<string, Snapshot?> snapshots = await yahooQuotes.GetSnapshotAsync(TICKERS);

foreach (KeyValuePair<string, Snapshot?> snapshot in snapshots)
{
    Console.WriteLine($"Snapshot Data For: {snapshot.Key}");

    if (snapshot.Value != null)
    {
        Console.WriteLine(snapshot.Value.LongName);
        Console.WriteLine(snapshot.Value.AverageAnalystRating);
    }
}

IEnumerable<string> RATINGS = new List<string>() { "1.9 - Buy", "2.8 - Hold", "1.3 - Strong Buy" };