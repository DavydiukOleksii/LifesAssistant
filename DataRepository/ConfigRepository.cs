using DataModel.Config;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace DataRepository
{
    public class ConfigRepository: ARepository
    {
        #region Singleton
        protected static ConfigRepository instance = null;
        public static ConfigRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new ConfigRepository();
                return instance;
            }
        }
        #endregion

        #region Constructor
        protected ConfigRepository()
        {
            CheckFolderExists(configFolderPath);
           // CheckFolderExists(resourceFolderPath);
        }
        #endregion

        #region Data

        protected string fileName = "config.json";

        #endregion

        #region Methods
        public List<LanguageItem> GetLanguageList()
        {
            List<LanguageItem> result;
            result = new List<LanguageItem> { new LanguageItem() { Title = "en", Value = "en-US" } };

            try
            {
                CheckFileExists(configFolderPath + fileName);

                using (StreamReader r = new StreamReader(configFolderPath + fileName))
                {
                    string json = r.ReadToEnd();

                    AllConfigData allConfig = JsonConvert.DeserializeObject<AllConfigData>(json);

                    if (allConfig != null && allConfig.LanguageList != null)
                    {
                        result = allConfig.LanguageList;
                    }
                }
                return result;
            }
            catch
            {
                return result;
            }
        }

        public List<string> GetThemeList()
        {
            List<string> result;
            result = new List<string> { "default" };
            try
            {

                CheckFileExists(configFolderPath + fileName);

                using (StreamReader r = new StreamReader(configFolderPath + fileName))
                {
                    string json = r.ReadToEnd();

                    AllConfigData allConfig = JsonConvert.DeserializeObject<AllConfigData>(json);

                    if (allConfig != null && allConfig.ThemeList != null)
                    {
                        result = allConfig.ThemeList;
                    }
                }
                return result;
            }
            catch
            {
                return result;
            }
        }

        public ConfigData GetCurrentConfig()
        {
            
            ConfigData result;
            result = new ConfigData()
            {
                Capasity = 5,
                Theme = "default",
                Language = new LanguageItem()
                {
                    Title = "en",
                    Value = "en-US"
                }
            };

            try
            {
                CheckFileExists(configFolderPath + fileName);

                using (StreamReader r = new StreamReader(configFolderPath + fileName))
                {
                    string json = r.ReadToEnd();

                    AllConfigData allConfig = JsonConvert.DeserializeObject<AllConfigData>(json);

                    if (allConfig != null)
                    {
                        if (allConfig.CurrentConfigData != null)
                        {
                            result = allConfig.CurrentConfigData;
                        }
                        else
                        {
                            if (allConfig.DefaultConfigData != null)
                            {
                                result = allConfig.DefaultConfigData;
                            }
                        }
                    }

                }
                return result;
            }
            catch
            {
                return result;
            }
        }

        public ConfigData GetDefaultConfig()
        {

            ConfigData result;
            result = new ConfigData()
            {
                Capasity = 5,
                Theme = "default",
                Language = new LanguageItem()
                {
                    Title = "en",
                    Value = "en-US"
                }
            };

            try
            {
                CheckFileExists(configFolderPath + fileName);

                using (StreamReader r = new StreamReader(configFolderPath + fileName))
                {
                    string json = r.ReadToEnd();

                    AllConfigData allConfig = JsonConvert.DeserializeObject<AllConfigData>(json);

                    if (allConfig != null)
                    {
                        if (allConfig.DefaultConfigData != null)
                        {
                            result = allConfig.DefaultConfigData;
                        }
                    }

                }
                return result;
            }
            catch
            {
                return result;
            }
        }

        public void SetCurrentConfig(ConfigData newConfig)
        {
            try
            {
                string newJson = "";

                CheckFileExists(configFolderPath + fileName);

                using (StreamReader r = new StreamReader(configFolderPath + fileName))
                {
                    string json = r.ReadToEnd();

                    AllConfigData allConfig = JsonConvert.DeserializeObject<AllConfigData>(json);
                    if (allConfig != null)
                    {
                        allConfig.CurrentConfigData = newConfig;
                    }
                    else
                    {
                        allConfig = new AllConfigData()
                        {
                            CurrentConfigData = newConfig,
                            LanguageList = new List<LanguageItem>() { newConfig.Language },
                            ThemeList = new List<string>() { newConfig.Theme },
                            DefaultConfigData = newConfig
                        };
                    }
                    newJson = JsonConvert.SerializeObject(allConfig);
                }

                File.WriteAllText(configFolderPath + fileName, newJson);
            }
            catch{}
        }

        public void SetDefaultConfig()
        {
            try
            {
                string newJson = "";

                CheckFileExists(configFolderPath + fileName);

                using (StreamReader r = new StreamReader(configFolderPath + fileName))
                {
                    AllConfigData allConfig =  new AllConfigData() 
                    {
                        LanguageList = new List<LanguageItem>()
                        {
                            new LanguageItem()
                            {
                                Title = "en",
                                Value = "en-Us"
                            },
                            new LanguageItem()
                            {
                                Title = "ua",
                                Value = "uk-Ua"
                            },
                            new LanguageItem()
                            {
                                Title = "ru",
                                Value = "ru-Ru"
                            }
                        },
                        ThemeList = new List<string>() { "default" },

                        CurrentConfigData = new ConfigData
                        {
                            Capasity = 4,
                            Theme = this.GetThemeList()[0],
                            Language = this.GetLanguageList()[0]
                        }
                    };

                    newJson = JsonConvert.SerializeObject(allConfig);
                }
                File.WriteAllText(configFolderPath + fileName, newJson);
            }
            catch { }
        }
        #endregion
    }
}