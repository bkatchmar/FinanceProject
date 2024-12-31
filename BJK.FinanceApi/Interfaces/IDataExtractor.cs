namespace BJK.FinanceApi.Interfaces
{
    internal interface IDataExtractor
    {
        IEnumerable<string> Symbols { get; }
        IEnumerable<IFinanceInstrument> InstrumentsInformation { get; }
        void GetInformation();
    }
}