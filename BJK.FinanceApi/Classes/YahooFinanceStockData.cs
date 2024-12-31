namespace BJK.FinanceApi.Classes
{
    using BJK.FinanceApi.Interfaces;
    using System.Text.RegularExpressions;
    using YahooQuotesApi;

    public class YahooFinanceStockData : IFinanceInstrument
    {
        public bool Filled { get; } = false;
        public string Name { get; } = string.Empty;
        public string Symbol { get; } = string.Empty;
        public string AnalystRating { get; } = string.Empty;
        public Snapshot? YahooSnapshopData { get; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj is string ticker)
            {
                return Symbol.ToUpper().Trim() == ticker.ToUpper().Trim();
            }
            else if (obj is IFinanceInstrument financeInstrument)
            {
                return Symbol.ToUpper().Trim() == financeInstrument.Symbol.ToUpper().Trim();
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Symbol.GetHashCode();
        }

        public YahooFinanceStockData(Snapshot? Data)
        {
            YahooSnapshopData = Data;

            if (Data != null)
            {
                Filled = true;
                Name = Data.LongName;
                Symbol = Data.Symbol.Name;
                AnalystRating = GetRatingFromYahooString(Data.AverageAnalystRating);
            }
        }

        private string GetRatingFromYahooString(string Rating)
        {
            Regex regex = new Regex(@"^[\d.]+\s*-\s*(.+)$");
            Match match = regex.Match(Rating);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return string.Empty;
        }
    }
}