using BJK.TickerExtract.Interfaces;

namespace BJK.TickerExtract.Classes
{
    public class ExampleTickerCollector : ITickerCollector
    {
        public IEnumerable<string> Tickers => ["AAPL", "NVDA", "TSLA"];
        public IEnumerable<string> Lines => [];
        public void Read(IReaderConfig ReaderConfig)
        {

        }
        public Task ReadAsync(IReaderConfig ReaderConfig)
        {
            Read(ReaderConfig);
            return Task.CompletedTask;
        }
    }
}
