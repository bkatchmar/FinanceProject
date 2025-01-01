namespace BJK.TickerExtract.Classes
{
    using BJK.TickerExtract.Interfaces;
    using Newtonsoft.Json;
    public class GetPersonalData
    {
        private const string READER_FILE = "PersonalData.json";
        private static PersonalDataConfig? config;

        public static IPersonalData Configuration => GetConfig();

        private static IPersonalData GetConfig()
        {
            MakeSureFileExists();

            if (config == null)
            {
                string baseDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Resources";
                string fileName = string.Concat(baseDirectory, "\\", READER_FILE);

                using StreamReader reader = new(fileName);
                string fileData = reader.ReadToEnd();
                reader.Close();

                config = JsonConvert.DeserializeObject<PersonalDataConfig>(fileData);
            }

            return config;
        }

        private static void MakeSureFileExists()
        {
            string baseDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Resources";
            string fileName = string.Concat(baseDirectory, "\\", READER_FILE);

            if (!Directory.Exists(baseDirectory))
            {
                Directory.CreateDirectory(baseDirectory);
            }

            if (!File.Exists(fileName))
            {
                PersonalDataConfig defaultConfig = new();
                string json = JsonConvert.SerializeObject(defaultConfig);
                using StreamWriter sw = new(fileName, false);
                sw.WriteLine(json);
                sw.Close();
            }
        }
    }
}