using BJK.TickerExtract.Interfaces;

namespace BJK.TickerExtract.Classes;

public class CsvTickerCollector : ITickerCollector
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
                if (line != null)
                {
                    string[] parts = line.Split(",");
                    if (IsThisLineActualTickerWeCanUse(parts))
                    {
                        lines.Add(line);
                        tickers.Add(parts[0]);
                    }
                }
            }
        }
    }

    private bool IsThisLineActualTickerWeCanUse(string[] Parts)
    {
        if (Parts.Length == 8)
        {
            if (!string.IsNullOrEmpty(Parts[0]) && !string.IsNullOrEmpty(Parts[1])
                && string.IsNullOrEmpty(Parts[2]) && string.IsNullOrEmpty(Parts[3])
                && string.IsNullOrEmpty(Parts[4]) && string.IsNullOrEmpty(Parts[5]) && string.IsNullOrEmpty(Parts[6])
                && string.IsNullOrEmpty(Parts[7]) && !IsDate(Parts[1]))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsDate(string Input)
    {
        // Try to parse the string as a date using DateTime.TryParse
        return DateTime.TryParse(Input, out _);
    }
}