namespace BJK.Finance.DecisionMaking.Classes
{
    using BJK.Finance.DecisionMaking.Interfaces;
    using BJK.FinanceApi.Interfaces;
    using BJK.TickerExtract.Interfaces;

    public class DecisionMaker(IPersonalData Personal, IEnumerable<IFinanceInstrument> InstrumentData) : IAutomateDecision
    {
        private readonly List<IOptionStrategyPossibility> possibleMoves = [];
        public IPersonalData PersonalDataConfig { get; } = Personal;
        public IEnumerable<IFinanceInstrument> InstrumentsInformation { get; } = InstrumentData;
        public IEnumerable<IOptionStrategyPossibility> PossibleOptionsStrategies => possibleMoves;
        public bool IncludeCoverCalls { get; set; } = true;
        public bool IncludeCashSecuredPuts { get; set; } = true;
        public void BuildStrategies()
        {
            int contactsWillingToBuy = PersonalDataConfig.MinumumUnitsToBuy / 100;

            List<IOptionStrategyPossibility> coverCalls = [];
            List<IOptionStrategyPossibility> cashSecuredPuts = [];

            foreach (IFinanceInstrument financeInstrument in InstrumentsInformation)
            {
                IOptionStrategyPossibility coverCall = new CoverCallPossibleTrade(PersonalDataConfig, financeInstrument);
                coverCall.Build();
                coverCalls.Add(coverCall);

                IOptionStrategyPossibility cashSecuredPut = new CashSecuredPutPossibleTrade(PersonalDataConfig, financeInstrument);
                cashSecuredPut.Build();
                cashSecuredPuts.Add(cashSecuredPut);
            }

            if (IncludeCoverCalls)
            {
                possibleMoves.AddRange(coverCalls.Where(s => s.ContractsCanAfford >= contactsWillingToBuy && PersonalDataConfig.RatingsTolerance.ToList().Contains(s.FinanceInstrument.AnalystRating)).OrderByDescending(s => s.ContractsCanAfford));
            }

            if (IncludeCashSecuredPuts)
            {
                possibleMoves.AddRange(cashSecuredPuts.Where(s => s.ContractsCanAfford >= contactsWillingToBuy && PersonalDataConfig.RatingsTolerance.ToList().Contains(s.FinanceInstrument.AnalystRating)).OrderByDescending(s => s.ContractsCanAfford));
            }
        }
    }
}