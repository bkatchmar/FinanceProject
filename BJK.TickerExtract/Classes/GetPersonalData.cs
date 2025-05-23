﻿namespace BJK.TickerExtract.Classes
{
    using BJK.TickerExtract.Interfaces;
    using Newtonsoft.Json;
    using Microsoft.Extensions.Configuration;

    public class GetPersonalData
    {
        private const string READER_FILE = "PersonalData.json";
        private const string FOLDER_NAME = "FinanceDecisionMaker";
        private const string APP_SETTINGS = "appsettings.json";
        private static PersonalDataConfig? config;

        public static IPersonalData Configuration => GetConfigFromAppSettingsFile();

        private static IPersonalData GetConfigFromAppSettingsFile()
        {
            if (config == null)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(APP_SETTINGS, optional: false, reloadOnChange: true)
                    .Build();

                PersonalDataConfig? readFromAppsettings = configuration.GetSection("PersonalData").Get<PersonalDataConfig>();

                if (readFromAppsettings != null)
                {
                    config = readFromAppsettings;
                }
            }
            
            return config ?? new PersonalDataConfig();
        }
        private static IPersonalData GetConfig()
        {
            MakeSureFileExists();

            if (config == null)
            {
                string baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + FOLDER_NAME;
                string fileName = string.Concat(baseDirectory, "\\", READER_FILE);

                using StreamReader reader = new(fileName);
                string fileData = reader.ReadToEnd();
                reader.Close();

                config = JsonConvert.DeserializeObject<PersonalDataConfig>(fileData);
            }

            return config ?? new PersonalDataConfig();
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
                PersonalDataConfig defaultConfig = new();
                string json = JsonConvert.SerializeObject(defaultConfig);
                using StreamWriter sw = new(fileName, false);
                sw.WriteLine(json);
                sw.Close();
            }
        }
    }
}