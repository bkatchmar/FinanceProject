namespace BJK.TickerExtract.Classes
{
    using BJK.TickerExtract.Interfaces;
    using Newtonsoft.Json;
    using Microsoft.Extensions.Configuration;

    public static class GetManualTickerData
    {
        private const string READER_FILE = "ManualTickers.json";
        private const string FOLDER_NAME = "FinanceDecisionMaker";
        private const string APP_SETTINGS = "appsettings.json";
        private static ManualTickerConfiguration? config;

        public static IManualTickerConfig Configuration => GetConfigFromAppSettingsFile();

        private static IManualTickerConfig GetConfigFromAppSettingsFile()
        {
            if (config == null)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(APP_SETTINGS, optional: false, reloadOnChange: true)
                    .Build();

                ManualTickerConfiguration? manualTickerConfig = configuration.GetSection("ManualTickers").Get<ManualTickerConfiguration>();

                if (manualTickerConfig != null)
                {
                    config = manualTickerConfig;
                }
            }
            return config ?? new ManualTickerConfiguration();
        }
        private static IManualTickerConfig GetConfig()
        {
            MakeSureFileExists();

            if (config == null)
            {
                string baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + FOLDER_NAME;
                string fileName = string.Concat(baseDirectory, "\\", READER_FILE);

                using StreamReader reader = new(fileName);
                string fileData = reader.ReadToEnd();
                reader.Close();

                config = JsonConvert.DeserializeObject<ManualTickerConfiguration>(fileData);
            }

            return config ?? new ManualTickerConfiguration();
        }

        private static void MakeSureFileExists()
        {
            string baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + FOLDER_NAME;
            string fileName = string.Concat(baseDirectory, "\\", READER_FILE);

            if (!Directory.Exists(baseDirectory))
            {
                Directory.CreateDirectory(baseDirectory);
            }

            if (!File.Exists(fileName))
            {
                ManualTickerConfiguration defaultConfig = new();
                string json = JsonConvert.SerializeObject(defaultConfig);
                using StreamWriter sw = new(fileName, false);
                sw.WriteLine(json);
                sw.Close();
            }
        }
    }
}