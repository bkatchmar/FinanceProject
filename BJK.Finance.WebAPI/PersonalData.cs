namespace BJK.Finance.WebAPI
{
    using BJK.TickerExtract.Interfaces;

    public class PersonalData : IPersonalData
    {
        public decimal UninvestedCash { get; set; } = 0;
        public int MinumumUnitsToBuy { get; set; } = 0;
        public string[] RatingsTolerance { get; set; } = [];
    }
}