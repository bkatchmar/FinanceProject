namespace BJK.Finance.DecisionMaking.Interfaces
{
    using BJK.FinanceApi.Interfaces;
    using BJK.TickerExtract.Interfaces;

    public interface IAutomateDecision
    {
        IPersonalData PersonalDataConfig { get; }
        IEnumerable<IFinanceInstrument> InstrumentsInformation { get; }
    }
}