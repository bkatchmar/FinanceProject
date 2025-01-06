using BJK.TickerExtract.Interfaces;

namespace BJK.TickerExtract.Classes;

public class CboeWeeklyCsvDownloader : ITickerCollector
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
    public async Task ReadAsync(IReaderConfig ReaderConfig)
    {
        // Download CSV data
        HttpClient httpClient = new();
        using Stream fileStream = await httpClient.GetStreamAsync(ReaderConfig.WeeklyCsvFileDownload);
        using StreamReader reader = new(fileStream);

        // string content = await reader.ReadToEndAsync();
        
        while (!reader.EndOfStream)
        {
            string? line = reader.ReadLine();
            if (line != null)
            {
                string[] parts = line.Split(",");
                if (IsThisLineActualTickerWeCanUse(parts))
                {
                    lines.Add(line);
                    tickers.Add(parts[0].Replace("\"",""));
                }
            }
        }
    }

    private bool IsThisLineActualTickerWeCanUse(string[] Parts)
    {
        if (Parts.Length == 2)
        {
            if (!string.IsNullOrEmpty(Parts[0]) && !string.IsNullOrEmpty(Parts[1]) && !IsDate(Parts[1]))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsDate(string? Input)
    {
        if (string.IsNullOrEmpty(Input))
        {
            return false;
        }

        // Try to parse the string as a date using DateTime.TryParse
        return DateTime.TryParse(Input, out _);
    }
}