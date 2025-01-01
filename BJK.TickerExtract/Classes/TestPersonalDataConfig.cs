namespace BJK.TickerExtract.Classes
{
    using BJK.TickerExtract.Interfaces;

    public class TestPersonalDataConfig : IPersonalData
    {
        public decimal UninvestedCash { get; } = 1000;
        public int MinumumUnitsToBuy { get; } = 200;
        public string[] RatingsTolerance { get; } = ["Hold", "Buy", "Strong Buy"];
    }
}