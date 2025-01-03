namespace BJK.TickerExtract.Classes
{
    using BJK.TickerExtract.Interfaces;
    using Newtonsoft.Json;

    public static class GetManualTickerData
    {
        private const string READER_FILE = "ManualTickers.json";
        private const string FOLDER_NAME = "FinanceDecisionMaker";
        private static ManualTickerConfiguration? config;

        public static IManualTickerConfig Configuration => GetConfig();

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