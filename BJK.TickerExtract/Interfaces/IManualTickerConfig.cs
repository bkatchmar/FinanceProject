namespace BJK.TickerExtract.Interfaces
{
    public interface IManualTickerConfig
    {
        string[] ToOmit {  get; }
        string[] ToAdd { get; }
    }
}