namespace BJK.Finance.DecisionMaking.Classes
{
    using BJK.Finance.DecisionMaking.Interfaces;
    using BJK.FinanceApi.Interfaces;
    using BJK.TickerExtract.Interfaces;

    public class CashSecuredPutPossibleTrade(IPersonalData Personal, IFinanceInstrument Instrument) : IOptionStrategyPossibility
    {
        public string Strategy => "Cash Secured Put";
        public int ContractsCanAfford { get; private set; } = 0;
        public IFinanceInstrument FinanceInstrument => Instrument;
        public IPersonalData PersonalDataConfig { get; } = Personal;
        public void Build()
        {
            if (PersonalDataConfig.UninvestedCash > FinanceInstrument.SamplePrice * 100 && FinanceInstrument.SamplePrice > 0.5M)
            {
                decimal nearestStrikePrice = RoundDownToNearestHalf(FinanceInstrument.SamplePrice);
                decimal amountOfCashNeededForStrikePrice = nearestStrikePrice * 100;
                decimal totalNumberOfContractsCanAfford = PersonalDataConfig.UninvestedCash / amountOfCashNeededForStrikePrice;
                ContractsCanAfford = (int)totalNumberOfContractsCanAfford;
            }
        }
        public override string ToString()
        {
            return $"{FinanceInstrument.Symbol} {Strategy}";
        }

        private decimal RoundDownToNearestHalf(decimal Value)
        {
            return Math.Floor(Value * 2) / 2;
        }
    }
}