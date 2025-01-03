namespace BJK.TickerExtract.Classes
{
    using BJK.TickerExtract.Interfaces;
    using Newtonsoft.Json;

    public static class GetReaderData
    {
        private const string READER_FILE = "ReaderConfig.json";
        private const string FOLDER_NAME = "FinanceDecisionMaker";
        private static ReaderConfiguration? config;

        public static IReaderConfig Configuration => GetConfig();

        private static IReaderConfig GetConfig()
        {
            MakeSureFileExists();

            if (config == null)
            {
                string baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + FOLDER_NAME;
                string fileName = string.Concat(baseDirectory, "\\", READER_FILE);

                using StreamReader reader = new(fileName);
                string fileData = reader.ReadToEnd();
                reader.Close();

                config = JsonConvert.DeserializeObject<ReaderConfiguration>(fileData);
            }

            return config ?? new ReaderConfiguration();
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
                ReaderConfiguration defaultConfig = new();
                string json = JsonConvert.SerializeObject(defaultConfig);
                using StreamWriter sw = new(fileName, false);
                sw.WriteLine(json);
                sw.Close();
            }
        }
    }
}