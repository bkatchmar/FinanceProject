namespace BJK.Finance.DecisionMaking.Classes
{
    using BJK.FinanceApi.Interfaces;

    public class TestFinanceInstrument : IFinanceInstrument
    {
        public bool Filled => true;
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public string AnalystRating { get; set; } = string.Empty;
        public decimal SamplePrice { get; set; } = decimal.Zero;
        public override string ToString() => Symbol;
    }
}