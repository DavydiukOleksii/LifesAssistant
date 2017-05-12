using System.Collections.Generic;

namespace DataModel.Config
{
    public class AllConfigData
    {
        public ConfigData CurrentConfigData { get; set; }
        public ConfigData DefaultConfigData { get; set; }
        public List<LanguageItem> LanguageList { get; set; }
        public List<string> ThemeList { get; set; }
    }
}
