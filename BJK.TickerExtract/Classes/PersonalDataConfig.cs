namespace BJK.TickerExtract.Classes
{
    using BJK.TickerExtract.Abstracts;
    using BJK.TickerExtract.Interfaces;

    public class PersonalDataConfig : ConfigurationReader, IPersonalData
    {
        public override string FileName => "PersonalData.json";
        public decimal UninvestedCash { get; set; } = decimal.Zero;
        public int MinumumUnitsToBuy { get; set; } = int.MinValue;
        public string[] RatingsTolerance { get; set; } = [];
    }
}