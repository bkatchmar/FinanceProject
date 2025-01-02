namespace BJK.Finance.DecisionMaking.Test
{
    using BJK.TickerExtract.Classes;
    using BJK.TickerExtract.Interfaces;
    using BJK.FinanceApi.Interfaces;
    using BJK.Finance.DecisionMaking.Classes;
    using BJK.Finance.DecisionMaking.Interfaces;

    public class Tests
    {
        private IPersonalData personalDataConfig;
        private List<IFinanceInstrument> hardcodedTestInstruments;

        [SetUp]
        public void Setup()
        {
            personalDataConfig = new TestPersonalDataConfig();
            
            hardcodedTestInstruments =
            [
                new TestFinanceInstrument()
                {
                    Name = "Apple Computer",
                    Symbol = "AAPL",
                    AnalystRating = "Buy",
                    SamplePrice = 250.41M
                },
                new TestFinanceInstrument()
                {
                    Name = "Tesla",
                    Symbol = "TSLA",
                    AnalystRating = "Hold",
                    SamplePrice = 401.93M
                },
                new TestFinanceInstrument()
                {
                    Name = "nVidia",
                    Symbol = "NVDA",
                    AnalystRating = "Strong Buy",
                    SamplePrice = 134.29M
                },
                new TestFinanceInstrument()
                {
                    Name = "Faraday Future Intelligent Electric Inc.",
                    Symbol = "FFIE",
                    AnalystRating = "N/A",
                    SamplePrice = 2.430M
                },
                new TestFinanceInstrument()
                {
                    Name = "Tilray Brands, Inc.",
                    Symbol = "TLRY",
                    AnalystRating = "Hold",
                    SamplePrice = 1.330M
                },
                new TestFinanceInstrument()
                {
                    Name = "Blink Charging Co.",
                    Symbol = "BLNK",
                    AnalystRating = "Buy",
                    SamplePrice = 1.39M
                },
                new TestFinanceInstrument()
                {
                    Name = "Bit Digital, Inc.",
                    Symbol = "BTBT",
                    AnalystRating = "Strong Buy",
                    SamplePrice = 2.93M
                },
                new TestFinanceInstrument()
                {
                    Name = "BBIG",
                    Symbol = "BBIG",
                    AnalystRating = "N/A",
                    SamplePrice = 0
                },
            ];
        }

        [Test]
        public void TestBasicPersonalData()
        {
            // I want to always assert this test data as my baselines
            Assert.Multiple(() =>
            {
                Assert.That(personalDataConfig.UninvestedCash, Is.EqualTo(1000));
                Assert.That(personalDataConfig.MinumumUnitsToBuy, Is.EqualTo(200));
                Assert.That(personalDataConfig.RatingsTolerance.Length, Is.EqualTo(3));
                Assert.That(personalDataConfig.RatingsTolerance, Contains.Item("Hold"));
                Assert.That(personalDataConfig.RatingsTolerance, Contains.Item("Buy"));
                Assert.That(personalDataConfig.RatingsTolerance, Contains.Item("Strong Buy"));
            });
        }

        [Test]
        public void TestFirstPhaseDecisionMakerCommands()
        {
            IAutomateDecision decisionMaker = new DecisionMaker(personalDataConfig, hardcodedTestInstruments);
            decisionMaker.BuildStrategies();

            IEnumerable<IOptionStrategyPossibility> coverCalls = decisionMaker.PossibleOptionsStrategies.Where(x => x.Strategy == "Cover Call");
            IEnumerable<IOptionStrategyPossibility> cashSecuredPuts = decisionMaker.PossibleOptionsStrategies.Where(x => x.Strategy == "Cash Secured Put");

            Assert.Multiple(() =>
            {
                Assert.That(decisionMaker.PossibleOptionsStrategies.Any(), Is.True);
                Assert.That(coverCalls.Any(), Is.True);
                Assert.That(coverCalls.Count(), Is.EqualTo(3));
                Assert.That(cashSecuredPuts.Any(), Is.True);
                Assert.That(cashSecuredPuts.Count(), Is.EqualTo(3));
            });
        }
    }
}