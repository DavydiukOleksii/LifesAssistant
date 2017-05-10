using System.Threading;
using System.Windows.Controls;
using System.Windows.Markup;

namespace LifesAssistant.View.ViewElements
{
    /// <summary>
    /// Interaction logic for CalendarTab.xaml
    /// </summary>
    public partial class CalendarTab : UserControl
    {
        public CalendarTab()
        {
            this.Language = XmlLanguage.GetLanguage(
                        Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);
            InitializeComponent();
        }
    }
}
