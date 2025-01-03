namespace BJK.TickerExtract.Classes
{
    using BJK.TickerExtract.Abstracts;
    using BJK.TickerExtract.Interfaces;

    public class BrianCustomManualTickerConfiguration : ConfigurationReader, IManualTickerConfig
    {
        public override string FileName => "ManualTickers.json";
        public string[] ToOmit { get; set; } = ["GSAT","LIST","BBAI","BBIG", "VERU", "SHOT", "WKHS"];
        public string[] ToAdd { get; set; } = ["BITO"];
    }
}