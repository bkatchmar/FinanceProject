namespace BJK.Finance.WebAPI
{
    using BJK.TickerExtract.Interfaces;

    public class AppSettings : IReaderConfig
    {
        public string WeeklyCsvFileDownload { get; set; } = string.Empty;
        public PersonalData? PersonalData { get; set; }
        public string[] TickersToOmit { get; set; } = [];
        public string? FileNameToReadFrom { get; set; } = string.Empty;
        public string? FileToWriteTo { get; set; } = string.Empty;
        public string? NextMovesFile { get; set; } = string.Empty;
    }
}