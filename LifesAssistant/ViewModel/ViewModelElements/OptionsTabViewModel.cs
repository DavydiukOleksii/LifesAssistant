using DataModel.Config;
using DataRepository;
using LifesAssistant.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            SelectedLanguage = LanguageList.FirstOrDefault( x => x.Equals(SelectedConfig.Language));

            IsRestartMessageVisible = Visibility.Hidden;
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

        protected LanguageItem _selectedLanguage;
        public LanguageItem SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged("SelectedLanguage");
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

        protected Visibility _isRestartMessageVisible;
        public Visibility IsRestartMessageVisible
        {
            get { return _isRestartMessageVisible; }
            set
            {
                _isRestartMessageVisible = value;
                OnPropertyChanged("IsRestartMessageVisible");
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
            SelectedConfig = ConfigRepository.Instance.GetDefaultConfig();
            SelectedLanguage = LanguageList.FirstOrDefault(x => x.Equals(SelectedConfig.Language));
        }
        #endregion

        #region Cancel
        private ICommand _cancelCommand;
        public ICommand Cancel
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand(ExecuteCancelCommand, CanExecuteCancelCommand));
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
            SelectedConfig = ConfigRepository.Instance.GetCurrentConfig();
            SelectedLanguage = LanguageList.FirstOrDefault(x => x.Equals(SelectedConfig.Language));
        }
        #endregion

        #region Apply
        private ICommand _applyCommand;
        public ICommand Apply
        {
            get
            {
                if (_applyCommand == null)
                {
                    _applyCommand = new RelayCommand(ExecuteApplyCommand, CanExecuteApplyCommand);
                }
                return _applyCommand;
            }
        }

        public bool CanExecuteApplyCommand(object parametr)
        {
            if (SelectedConfig != null)
                return true;
            else
                return false;
        }

        public void ExecuteApplyCommand(object parametr)
        {
            SelectedConfig.Language = SelectedLanguage;
            ConfigRepository.Instance.SetCurrentConfig(SelectedConfig);
            CurrentConfig = ConfigRepository.Instance.GetCurrentConfig();

            IsRestartMessageVisible = Visibility.Visible;
        }
        #endregion

        #region Restart
        private ICommand _restartCommand;
        public ICommand Restart
        {
            get
            {
                if (_restartCommand == null)
                {
                    _restartCommand = new RelayCommand(ExecuteRestartCommand);
                }
                return _restartCommand;
            }
        }

        public void ExecuteRestartCommand(object parametr)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
        #endregion

        #endregion

        #region FreeData
        #endregion
    }
}
