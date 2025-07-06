using BJK.Finance.DecisionMaking.Classes;
using BJK.Finance.DecisionMaking.Interfaces;
using BJK.Finance.WebAPI.Models;
using BJK.FinanceApi.Classes;
using BJK.TickerExtract.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BJK.Finance.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private readonly AppSettings _appSettings;

        public DataController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value; // Access the settings
        }

        [HttpGet]
        public async Task<ActionResult<List<Strategy>>> GetTickerData()
        {
            List<Strategy> rtnVal = [];

            // Assuming the above code did not run and we have a destination file to write to, run the rest of the program
            CboeWeeklyCsvDownloader tickerCollector = new();
            await tickerCollector.ReadAsync(_appSettings);

            // Call the API
            List<string> allTickers = tickerCollector.Tickers.ToList();
            foreach (string toOmit in _appSettings.TickersToOmit)
            {
                allTickers.Remove(toOmit);
            }

            YahooQuotesApiCaller yahooExtractor = new(allTickers);

            // Call data extractor
            await yahooExtractor.GetInformation();

            if (_appSettings.PersonalData != null)
            {
                // Start looking into the decision maker
                DecisionMaker decisionMaker = new(_appSettings.PersonalData, yahooExtractor.InstrumentsInformation)
                {
                    IncludeCoverCalls = false
                };
                decisionMaker.BuildStrategies();

                foreach (IOptionStrategyPossibility nextMove in decisionMaker.PossibleOptionsStrategies)
                {
                    rtnVal.Add(new()
                    {
                        Symbol = nextMove.FinanceInstrument.Symbol,
                        Name = nextMove.FinanceInstrument.Name,
                        AnalystRating = nextMove.FinanceInstrument.AnalystRating,
                        TypeOfStrategy = nextMove.Strategy,
                        ContractsCanAfford = nextMove.ContractsCanAfford
                    });
                }
            }

            return Ok(rtnVal);
        }

        [HttpPost]
        public async Task<ActionResult<List<Strategy>>> GetTickerData([FromBody] TickerRequest request)
        {
            List<Strategy> rtnVal = [];

            // Assuming the above code did not run and we have a destination file to write to, run the rest of the program
            CboeWeeklyCsvDownloader tickerCollector = new();
            await tickerCollector.ReadAsync(_appSettings);

            // Call the API
            List<string> allTickers = tickerCollector.Tickers.ToList();
            foreach (string toOmit in request.TickersToOmit)
            {
                allTickers.Remove(toOmit);
            }

            YahooQuotesApiCaller yahooExtractor = new(allTickers);

            // Call data extractor
            await yahooExtractor.GetInformation();

            // Start looking into the decision maker
            DecisionMaker decisionMaker = new(request.PersonalData, yahooExtractor.InstrumentsInformation)
            {
                IncludeCoverCalls = request.IncludeCoverCalls,
                IncludeCashSecuredPuts = request.IncludeCashSecuredPuts
            };
            decisionMaker.BuildStrategies();

            foreach (IOptionStrategyPossibility nextMove in decisionMaker.PossibleOptionsStrategies)
            {
                rtnVal.Add(new()
                {
                    Symbol = nextMove.FinanceInstrument.Symbol,
                    Name = nextMove.FinanceInstrument.Name,
                    AnalystRating = nextMove.FinanceInstrument.AnalystRating,
                    TypeOfStrategy = nextMove.Strategy,
                    ContractsCanAfford = nextMove.ContractsCanAfford
                });
            }

            return Ok(rtnVal);
        }
    }
}
