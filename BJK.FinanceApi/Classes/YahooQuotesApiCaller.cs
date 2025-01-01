namespace BJK.FinanceApi.Classes
{
    using BJK.FinanceApi.Interfaces;
    using YahooQuotesApi;

    public class YahooQuotesApiCaller(IEnumerable<string> Symbols) : IDataExtractor
    {
        public IEnumerable<string> Symbols { get; } = Symbols;
        public IEnumerable<IFinanceInstrument> InstrumentsInformation => financeInstruments;

        private List<IFinanceInstrument> financeInstruments = [];

        public async Task GetInformation()
        {
            YahooQuotes yahooQuotes = new YahooQuotesBuilder().Build();
            Dictionary<string, Snapshot?> snapshots = await yahooQuotes.GetSnapshotAsync(Symbols);

            foreach (KeyValuePair<string, Snapshot?> snapshot in snapshots)
            {
                IFinanceInstrument current = new YahooFinanceStockData(snapshot.Value);
                if (current.Filled)
                {
                    financeInstruments.Add(current);
                }
            }
        }

        public bool DoesInformationForFinancialInstrumentExists(string Symbol)
        {
            return financeInstruments.Any(i => i.Symbol == Symbol);
        }

        public IFinanceInstrument? GetFinanceInstrument(string Symbol)
        {
            return financeInstruments.Find(i => i.Symbol == Symbol);
        }
    }
}