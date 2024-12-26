using System.Windows;
using System.Windows.Media;

namespace DishesCompany
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Resources["AccentColor"] = new SolidColorBrush(Color.FromRgb(73, 140, 81));
            Resources["SecondaryBackgroundColor"] = new SolidColorBrush(Color.FromRgb(118, 227, 131));
        }
    }
}
