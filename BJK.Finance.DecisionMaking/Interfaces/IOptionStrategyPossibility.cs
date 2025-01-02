namespace BJK.Finance.DecisionMaking.Interfaces
{
    using BJK.FinanceApi.Interfaces;
    using BJK.TickerExtract.Interfaces;

    public interface IOptionStrategyPossibility
    {
        string Strategy { get; }
        int ContractsCanAfford { get; }
        IFinanceInstrument FinanceInstrument { get; }
        IPersonalData PersonalDataConfig { get; }
        void Build();
    }
}