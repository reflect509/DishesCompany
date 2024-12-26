using DishesCompany.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace DishesCompany.View
{
    /// <summary>
    /// Логика взаимодействия для CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder : Page
    {
        public MainWindow mainWindow;
        private Users user;
        public CreateOrder(MainWindow mainWindow, Users user, ObservableCollection<Products> products)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.user = user;
            List<Delivery_places> delivery_places = DatabaseControl.GetDelivery_places();
            OrderViewModel orderViewModel = new OrderViewModel(user)
            {
                Products = new ObservableCollection<Products>(products),
                Delivery_places = new ObservableCollection<Delivery_places>(delivery_places),
                Product_orders = new ObservableCollection<Product_orders>()
            };
            this.DataContext = orderViewModel;
            MakeProductOrders();
        }
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.Pages.Login);
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.Pages.MainApp, user);
        }
        private void IncrementClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel panel = (StackPanel)button.Parent;
            StackPanel parentPanel = (StackPanel)panel.Parent;
            TextBox textBox = (TextBox)parentPanel.Children[1];
            if (int.TryParse(textBox.Text, out int value))
            {
                textBox.Text = (value + 1).ToString();
                Product_orders productOrder = (Product_orders)textBox.DataContext;
                productOrder.Amount = value + 1;
            }
            else
            {
                textBox.Text = "1";
            }
        }
        private void DecrementClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel panel = (StackPanel)button.Parent;
            StackPanel parentPanel = (StackPanel)panel.Parent;
            TextBox textBox = (TextBox)parentPanel.Children[1];
            if (int.TryParse(textBox.Text, out int value) && value > 1)
            {
                textBox.Text = (value - 1).ToString();
                Product_orders productOrder = (Product_orders)textBox.DataContext;
                productOrder.Amount = value - 1;
            }
            else
            {
                textBox.Text = "1";
            }
        }

        private void MakeOrder(object sender, RoutedEventArgs e)
        {
            if (DeliveryList.SelectedItem != null)
            {
                Orders order = new Orders(
                    DateOnly.FromDateTime(DateTime.Now),
                    null,
                    ((Delivery_places)DeliveryList.SelectedItem).Delivery_place_id,
                    user.User_id,
                    "Новый"
                    );

                bool CheckAmount = false;
                OrderViewModel orderViewModel = (OrderViewModel)this.DataContext;

                order.Product_order_entities = new List<Product_orders>();

                foreach (Product_orders product_order in orderViewModel.Product_orders)
                {
                    product_order.Order_id = order.Order_id;
                    if (product_order.Product_entity.Amount < product_order.Amount)
                    {
                        CheckAmount = true;
                        MessageBox.Show($"Товара {product_order.Product_entity.Product_name} не достаточно, осталось {product_order.Product_entity.Amount}");
                        break;
                    }
                    else
                    {
                        order.Product_order_entities.Add(product_order);
                    }
                }
                if (!CheckAmount)
                {
                    DatabaseControl.AddOrder(order);
                    MessageBox.Show("Заказ успешно создан");
                    foreach (Product_orders product_order in orderViewModel.Product_orders)
                    {
                        product_order.Product_entity.Amount -= product_order.Amount;
                        DatabaseControl.ChangeProductAmount(product_order.Product_entity);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберете место доставки");
            }
        }
        private void MakeProductOrders()
        {
            OrderViewModel orderViewModel = (OrderViewModel)this.DataContext;
            foreach (Products product in orderViewModel.Products)
            {
                Product_orders product_order = new Product_orders();
                product_order.Product_articul = product.Articul;
                product_order.Product_entity = product;
                orderViewModel.Product_orders.Add(product_order);
            }
        }
    }
}
