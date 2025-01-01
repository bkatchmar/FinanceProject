namespace BJK.Finance.DecisionMaking.Test
{
    using BJK.TickerExtract.Classes;
    using BJK.TickerExtract.Interfaces;
    using BJK.FinanceApi.Interfaces;
    using BJK.Finance.DecisionMaking.Classes;

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
            ];
        }

        [Test]
        public void TestBasicPersonalData()
        {
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
    }
}