namespace BJK.TickerExtract.Classes
{
    using BJK.TickerExtract.Abstracts;
    using BJK.TickerExtract.Interfaces;

    public class ManualTickerConfiguration : ConfigurationReader, IManualTickerConfig
    {
        public override string FileName => "ManualTickers.json";
        public string[] ToOmit { get; set; } = [];
        public string[] ToAdd { get; set; } = [];
    }
}