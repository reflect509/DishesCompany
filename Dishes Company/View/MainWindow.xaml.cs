using DishesCompany.View;
using DishesCompany.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace DishesCompany
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OpenPage(Pages.Login);
        }

        public enum Pages
        {
            Login,
            Regin,
            MainApp,
            EditProduct,
            AddProduct,
            ViewOrders,
            CreateOrder,
            ViewOrderDetails
        }

        public void OpenPage(Pages page)
        {
            if (page == Pages.Login)
            {
                frame.Navigate(new Login(this));
            }
            else if (page == Pages.Regin)
            {
                frame.Navigate(new Regin(this));
            }
        }
        public void OpenPage(Pages page, Users user)
        {
            if (page == Pages.MainApp)
            {
                ProductViewModel productViewModel = new ProductViewModel();
                frame.Navigate(new MainApp(this, user, productViewModel));
            }
            else if (page == Pages.AddProduct)
            {
                frame.Navigate(new AddProduct(this, user));
            }
            else if (page == Pages.ViewOrders)
            {
                OrderViewModel orderViewModel = new OrderViewModel(user);
                frame.Navigate(new ViewOrders(this, user, orderViewModel));
            }
        }
        public void OpenPage(Pages page, Users user, ObservableCollection<Products> products)
        {
            frame.Navigate(new CreateOrder(this, user, products));
        }
        public void OpenPage(Pages page, Products product, Users user)
        {
            frame.Navigate(new EditProduct(this, product, user));
        }
    }
}