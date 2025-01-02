namespace BJK.TickerExtract.Abstracts
{
    public abstract class ConfigurationReader
    {
        public virtual string FileName { get; } = "Config.json";
    }
}