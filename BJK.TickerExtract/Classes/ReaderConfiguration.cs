namespace BJK.TickerExtract.Classes
{
    using BJK.TickerExtract.Interfaces;

    public class ReaderConfiguration : IReaderConfig
    {
        public string FileNameToReadFrom { get; set; } = string.Empty;
        public string FileToWriteTo { get; set; } = string.Empty;
        public string NextMovesFile { get; set; } = string.Empty;
        public string WeeklyCsvFileDownload { get; set; } = string.Empty;
    }
}