namespace BJK.TickerExtract.Classes
{
    using BJK.TickerExtract.Interfaces;

    public class TickerCollector : ITickerCollector
    {
        private List<string> lines = [];
        private List<string> tickers = [];

        public IEnumerable<string> Tickers => tickers;
        public IEnumerable<string> Lines => lines;
        public void Read(IReaderConfig ReaderConfig)
        {
            if (!string.IsNullOrEmpty(ReaderConfig.FileNameToReadFrom) && File.Exists(ReaderConfig.FileNameToReadFrom))
            {
                using StreamReader reader = new(ReaderConfig.FileNameToReadFrom);

                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(line) && !line.Contains("Sector:"))
                    {
                        line = line.TrimStart();
                        line = line.TrimEnd();
                        string[] parts = line.Split(" ");
                        if (parts.Length > 0)
                        {
                            tickers.Add(parts[0]);
                        }
                    }
                }
            }
        }
    }
}