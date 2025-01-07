using BJK.TickerExtract.Interfaces;
using CsvHelper;
using System.Globalization;

namespace BJK.TickerExtract.Classes;

public class CboeWeeklyCsvDownloader : ITickerCollector
{
    private readonly List<string> lines = [];
    private readonly List<string> tickers = [];
    
    public IEnumerable<string> Tickers => tickers;
    public IEnumerable<string> Lines => lines;
    public void Read(IReaderConfig ReaderConfig)
    {
        ReadAsync(ReaderConfig).Wait();
    }
    public async Task ReadAsync(IReaderConfig ReaderConfig)
    {
        // Download CSV data
        string csvData = await DownloadCsvAsync(ReaderConfig.WeeklyCsvFileDownload);
        
        using var reader = new StringReader(csvData);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        while (csv.Read())
        {
            List<string?> parts = [];
            for (int i = 0; i < csv.ColumnCount; i++)
            {
                parts.Add(csv.GetField(i));
            }
            
            if (IsThisLineActualTickerWeCanUse([.. parts]))
            {
                string line = string.Join(",", parts.ToArray());
                lines.Add(line);

                string? possibleTicker = parts[0];
                if (!string.IsNullOrEmpty(possibleTicker))
                {
                    tickers.Add(possibleTicker);
                }
            }
        }
    }

    private static bool IsThisLineActualTickerWeCanUse(string?[] Parts)
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

    private static async Task<string> DownloadCsvAsync(string URL)
    {
        using HttpClient httpClient = new();
        using HttpResponseMessage response = await httpClient.GetAsync(URL);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to download CSV. Status code: {response.StatusCode}");
        }
        
        return await response.Content.ReadAsStringAsync();
    }
}