namespace BJK.FinanceApi.Interfaces
{
    internal interface IFinanceInstrument
    {
        bool Filled { get; }
        string Name { get; }
        string Symbol { get; }
        string AnalystRating { get; }
    }
}