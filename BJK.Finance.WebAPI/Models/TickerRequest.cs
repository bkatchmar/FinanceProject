namespace BJK.Finance.WebAPI.Models
{
    public class TickerRequest
    {
        public string[] TickersToOmit { get; set; } = [];
        public required PersonalData PersonalData { get; set; }
    }
}