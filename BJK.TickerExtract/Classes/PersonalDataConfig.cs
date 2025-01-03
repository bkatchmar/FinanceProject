namespace BJK.TickerExtract.Classes
{
    using BJK.TickerExtract.Interfaces;

    public class PersonalDataConfig : IPersonalData
    {
        public decimal UninvestedCash { get; set; } = decimal.Zero;
        public int MinumumUnitsToBuy { get; set; } = int.MinValue;
        public string[] RatingsTolerance { get; set; } = [];
    }
}