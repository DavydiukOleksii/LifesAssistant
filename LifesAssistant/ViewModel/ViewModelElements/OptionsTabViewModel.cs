using DataModel.Config;
using DataRepository;
using LifesAssistant.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LifesAssistant.ViewModel.ViewModelElements
{
    class OptionsTabViewModel: ViewModelBase
    {
        #region Constructor
        public OptionsTabViewModel()
        {
            LanguageList = ConfigRepository.Instance.GetLanguageList();
            ThemeList = ConfigRepository.Instance.GetThemeList();
            CurrentConfig = ConfigRepository.Instance.GetCurrentConfig();
            SelectedConfig = ConfigRepository.Instance.GetCurrentConfig();
            DefaultConfig = ConfigRepository.Instance.GetDefaultConfig();
        }
        #endregion

        #region Data
        protected List<LanguageItem> _languageList;
        public List<LanguageItem> LanguageList
        {
            get { return _languageList; }
            set
            {
                _languageList = value;
                OnPropertyChanged("LanguageList");
            }
        }

        protected List<string> _themeList;
        public List<string> ThemeList
        {
            get { return _themeList; }
            set
            {
                _themeList = value;
                OnPropertyChanged("ThemeList");
            }
        }

        protected ConfigData _selectedConfig;
        public ConfigData SelectedConfig
        {
            get { return _selectedConfig; }
            set
            {
                _selectedConfig = value;
                OnPropertyChanged("SelectedConfig");
            }
        }

        protected ConfigData _currentConfig;
        public ConfigData CurrentConfig
        {
            get { return _currentConfig; }
            set
            {
                _currentConfig = value;
                OnPropertyChanged("CurrentConfig");
            }
        }

        protected ConfigData _defaultConfig;
        public ConfigData DefaultConfig
        {
            get { return _defaultConfig; }
            set
            {
                _defaultConfig = value;
                OnPropertyChanged("DefaultConfig");
            }
        }

        #endregion

        #region Commands

        #region Set Default Config
        private ICommand _setDefaultConfigCommand;
        public ICommand SetDefaultConfig
        {
            get
            {
                if (_setDefaultConfigCommand == null)
                {
                    _setDefaultConfigCommand = new RelayCommand(ExecuteSetDefaultConfigCommand, CanExecuteSetDefaultConfigCommand);
                }
                return _setDefaultConfigCommand;
            }
        }

        public bool CanExecuteSetDefaultConfigCommand(object parametr)
        {
            return true;
        }

        public void ExecuteSetDefaultConfigCommand(object parametr)
        {
            SelectedConfig = DefaultConfig;
        }
        #endregion

        #region Cancel
        private ICommand _cancelCommand;
        public ICommand Cancel
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
                }
                return _cancelCommand;
            }
        }

        public bool CanExecuteCancelCommand(object parametr)
        {
            if (CurrentConfig != null)
                return true;
            else
                return false;
        }

        public void ExecuteCancelCommand(object parametr)
        {
            SelectedConfig = CurrentConfig;
        }
        #endregion

        #endregion

        #region FreeData
        #endregion
    }
}
