namespace BJK.TickerExtract.Interfaces
{
    public interface IReaderConfig
    {
        string FileNameToReadFrom {  get; }
        string FileToWriteTo { get; }
        string NextMovesFile { get; }
        string WeeklyCsvFileDownload { get; }
    }
}