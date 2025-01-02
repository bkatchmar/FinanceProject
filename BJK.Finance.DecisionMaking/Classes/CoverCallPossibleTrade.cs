namespace BJK.Finance.DecisionMaking.Classes
{
    using BJK.Finance.DecisionMaking.Interfaces;
    using BJK.FinanceApi.Interfaces;
    using BJK.TickerExtract.Interfaces;

    public class CoverCallPossibleTrade(IPersonalData Personal, IFinanceInstrument Instrument) : IOptionStrategyPossibility
    {
        public string Strategy => "Cover Call";
        public int ContractsCanAfford { get; private set; } = 0;
        public IFinanceInstrument FinanceInstrument => Instrument;
        public IPersonalData PersonalDataConfig { get; } = Personal;
        public void Build()
        {
            if (PersonalDataConfig.UninvestedCash > FinanceInstrument.SamplePrice && FinanceInstrument.SamplePrice > 0)
            {
                decimal totalNumberOfSharesCanAfford = PersonalDataConfig.UninvestedCash / FinanceInstrument.SamplePrice;
                if (totalNumberOfSharesCanAfford >= PersonalDataConfig.MinumumUnitsToBuy)
                {
                    ContractsCanAfford = (int)(totalNumberOfSharesCanAfford / 100);
                }
            }
        }
        public override string ToString()
        {
            return $"{FinanceInstrument.Symbol} {Strategy}";
        }
    }
}