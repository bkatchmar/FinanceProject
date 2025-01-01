namespace BJK.TickerExtract.Interfaces
{
    public interface IPersonalData
    {
        decimal UninvestedCash { get; }
        int MinumumUnitsToBuy { get; }
        string[] RatingsTolerance { get; }
    }
}