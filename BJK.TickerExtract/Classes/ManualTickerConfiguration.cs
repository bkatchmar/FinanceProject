namespace BJK.TickerExtract.Classes
{
    using BJK.TickerExtract.Interfaces;

    public class ManualTickerConfiguration : IManualTickerConfig
    {
        public string[] ToOmit { get; set; } = [];
        public string[] ToAdd { get; set; } = [];
    }
}