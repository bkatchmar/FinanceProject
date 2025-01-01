namespace BJK.FinanceApi.Test
{
    using BJK.FinanceApi.Classes;
    using BJK.FinanceApi.Interfaces;

    public class Tests
    {
        private IEnumerable<string> TICKERS;
        IDataExtractor yahooExtractor;

        [SetUp]
        public void Setup()
        {
            TICKERS = ["AAPL", "NVDA", "TSLA"];
            yahooExtractor = new YahooQuotesApiCaller(TICKERS);
        }

        [Test]
        public async Task TestBasicYahooExtraction()
        {
            // Call data extractor
            await yahooExtractor.GetInformation();

            Assert.Multiple(() =>
            {
                Assert.That(yahooExtractor.InstrumentsInformation.Count(), Is.EqualTo(3));
                Assert.That(yahooExtractor.DoesInformationForFinancialInstrumentExists("AAPL"), Is.True);
                Assert.That(yahooExtractor.DoesInformationForFinancialInstrumentExists("NVDA"), Is.True);
                Assert.That(yahooExtractor.DoesInformationForFinancialInstrumentExists("TSLA"), Is.True);
                Assert.That(yahooExtractor.DoesInformationForFinancialInstrumentExists("FFIE"), Is.False);
            });
        }

        [Test]
        public async Task TestYahooExtactionWithMoreDetails()
        {
            // Call data extractor
            await yahooExtractor.GetInformation();

            IFinanceInstrument? aapl = yahooExtractor.GetFinanceInstrument("AAPL");
            IFinanceInstrument? nvda = yahooExtractor.GetFinanceInstrument("NVDA");
            IFinanceInstrument? tsla = yahooExtractor.GetFinanceInstrument("TSLA");

            Assert.Multiple(() =>
            {
                Assert.That(aapl, Is.Not.Null);
                Assert.That(nvda, Is.Not.Null);
                Assert.That(tsla, Is.Not.Null);
                Assert.That(aapl?.Name, Is.EqualTo("Apple Inc."));
                Assert.That(aapl?.Equals("AAPL"), Is.True);
                Assert.That(nvda?.Name, Is.EqualTo("NVIDIA Corporation"));
                Assert.That(nvda?.Equals("NVDA"), Is.True);
                Assert.That(tsla?.Name, Is.EqualTo("Tesla, Inc."));
                Assert.That(tsla?.Equals("TSLA"), Is.True);
                Assert.That(aapl?.AnalystRating, Is.Not.Null);
                Assert.That(aapl?.AnalystRating, Is.Not.Empty);
                Assert.That(nvda?.AnalystRating, Is.Not.Null);
                Assert.That(nvda?.AnalystRating, Is.Not.Empty);
                Assert.That(tsla?.AnalystRating, Is.Not.Null);
                Assert.That(tsla?.AnalystRating, Is.Not.Empty);
                Assert.That(aapl?.SamplePrice, Is.GreaterThan(0));
                Assert.That(nvda?.SamplePrice, Is.GreaterThan(0));
                Assert.That(tsla?.SamplePrice, Is.GreaterThan(0));
            });
        }
    }
}
