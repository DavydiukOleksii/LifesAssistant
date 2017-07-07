using System.Windows;
using LifesAssistant.ViewModel;
using DataRepository;
using System.Threading;
using System.Globalization;
using DataModel.Config;
using System;

namespace LifesAssistant
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            try { 
                string cultureLanguage = "ua-Uk";
                //if (ConfigRepository.Instance.GetCurrentConfig().Language.Value != null)
                //{
                    cultureLanguage = ConfigRepository.Instance.GetCurrentConfig().Language.Value;
                //}
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureLanguage);
                //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                //Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
}

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                // base.OnStartup(e);
                MainWindow window = new MainWindow();
                MainWindowViewModel viewModel = new MainWindowViewModel();

                //binding context to window
                window.DataContext = viewModel;
                window.Show();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
