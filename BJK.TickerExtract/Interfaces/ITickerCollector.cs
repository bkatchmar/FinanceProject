namespace BJK.TickerExtract.Interfaces
{
    public interface ITickerCollector
    {
        IEnumerable<string> Tickers { get; }
        IEnumerable<string> Lines { get; }
        void Read(IReaderConfig ReaderConfig);
    }
}