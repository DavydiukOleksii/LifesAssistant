using System.Windows;
using LifesAssistant.ViewModel;
using DataRepository;
using System.Threading;
using System.Globalization;
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
           Thread.CurrentThread.CurrentUICulture = new CultureInfo(ConfigRepository.Instance?.GetCurrentConfig()?.Language.Value ?? "ua-Uk");
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

                if (!InstanceCheck())
                {
                    // нажаловаться пользователю и завершить процесс
                    MessageBox.Show("The application is already running!");
                    Application.Current.Shutdown(0);
                }

                window.Show();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        protected static Mutex InstanceCheckMutex;
        protected static bool InstanceCheck()
        {
            bool isNew;
            InstanceCheckMutex = new Mutex(true, "4dfa58e9-79aa-46aa-8272-91f678f99885", out isNew);
            return isNew;
        }
    }
}
