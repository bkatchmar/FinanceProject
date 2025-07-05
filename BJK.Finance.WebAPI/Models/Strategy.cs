namespace BJK.Finance.WebAPI.Models
{
    public class Strategy
    {
        public string Symbol { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string AnalystRating {  get; set; } = string.Empty;
        public string TypeOfStrategy { get; set; } = string.Empty;
        public int ContractsCanAfford { get; set; } = 0;
    }
}