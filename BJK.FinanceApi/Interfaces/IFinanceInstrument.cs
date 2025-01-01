namespace BJK.FinanceApi.Interfaces
{
    public interface IFinanceInstrument
    {
        bool Filled { get; }
        string Name { get; }
        string Symbol { get; }
        string AnalystRating { get; }
        decimal SamplePrice { get; }
    }
}