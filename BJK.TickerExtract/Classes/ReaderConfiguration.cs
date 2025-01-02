namespace BJK.TickerExtract.Classes
{
    using BJK.TickerExtract.Abstracts;
    using BJK.TickerExtract.Interfaces;

    public class ReaderConfiguration : ConfigurationReader, IReaderConfig
    {
        public override string FileName => "ReaderConfig.json";
        public string FileNameToReadFrom { get; set; } = string.Empty;
        public string FileToWriteTo { get; set; } = string.Empty;
        public string NextMovesFile { get; set; } = string.Empty;
    }
}