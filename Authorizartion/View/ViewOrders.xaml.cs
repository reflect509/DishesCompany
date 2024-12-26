using DishesCompany.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DishesCompany.View
{
    /// <summary>
    /// Логика взаимодействия для ViewOrders.xaml
    /// </summary>
    public partial class ViewOrders : Page
    {
        MainWindow mainWindow;
        Users user;
        public ViewOrders(MainWindow mainWindow, Users user, OrderViewModel orderViewModel)
        {
            this.mainWindow = mainWindow;
            this.DataContext = orderViewModel;
            this.user = user;

            InitializeComponent();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.Pages.Login);
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.Pages.MainApp, user);
        }
    }
}
