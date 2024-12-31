namespace BJK.TickerExtract.Interfaces
{
    public interface IReaderConfig
    {
        string FileNameToReadFrom {  get; }
        string FileToWriteTo { get; }
    }
}