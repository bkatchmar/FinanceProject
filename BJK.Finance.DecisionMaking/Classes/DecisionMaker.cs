namespace BJK.Finance.DecisionMaking.Classes
{
    using BJK.Finance.DecisionMaking.Interfaces;
    using BJK.FinanceApi.Interfaces;
    using BJK.TickerExtract.Interfaces;

    public class DecisionMaker(IPersonalData Personal, IEnumerable<IFinanceInstrument> InstrumentData) : IAutomateDecision
    {
        public IPersonalData PersonalDataConfig { get; } = Personal;
        public IEnumerable<IFinanceInstrument> InstrumentsInformation { get; } = InstrumentData;
    }
}