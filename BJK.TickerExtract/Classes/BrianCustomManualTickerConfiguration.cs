namespace BJK.TickerExtract.Classes
{
    using BJK.TickerExtract.Interfaces;

    public class BrianCustomManualTickerConfiguration : IManualTickerConfig
    {
        public string[] ToOmit { get; set; } = ["GSAT","LIST","BBAI","BBIG", "VERU", "SHOT", "WKHS"];
        public string[] ToAdd { get; set; } = ["BITO"];
    }
}