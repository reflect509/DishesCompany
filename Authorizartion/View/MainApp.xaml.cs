using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace DishesCompany
{
    /// <summary>
    /// Логика взаимодействия для MainApp.xaml
    /// </summary>
    public partial class MainApp : Page
    {
        public MainWindow mainWindow;
        private Users user;
        public MainApp(MainWindow mainwindow, Users user, ProductViewModel productViewModel)
        {
            InitializeComponent();

            this.mainWindow = mainwindow;
            this.DataContext = productViewModel;
            if (user != null)
            {
                this.user = user;

                FullName.Text = user.Full_name;

                if (user.Role_id != 3)
                {
                    AddProductButton.Visibility = Visibility.Collapsed;
                    EditProductButton.Visibility = Visibility.Collapsed;
                    DeleteProductButton.Visibility = Visibility.Collapsed;
                    ProductListBox.SelectionMode = SelectionMode.Multiple;
                }
                else if (user.Role_id == 3)
                {
                    CreateOrderButton.Visibility = Visibility.Collapsed;
                    GoToOrdersButton.Visibility = Visibility.Collapsed;
                    ProductListBox.SelectionMode = SelectionMode.Single;
                }
            }
        }
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.Pages.Login);
        }

        private void SortAscButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();

            string selectedManufacturer = ManufacturerComboBox.SelectedItem?.ToString();

            List<Products> sortedAndFilteredProducts =
                SearchAndFilterProducts(searchText, selectedManufacturer);

            ProductViewModel productViewModel = (ProductViewModel)this.DataContext;
            productViewModel.Products = new ObservableCollection<Products>(DatabaseControl.SortingProducts("asc", sortedAndFilteredProducts));
            productViewModel.CurrentProductCount = DatabaseControl.GetCount(productViewModel.Products.ToList());
        }

        private void SortDescButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();

            string selectedManufacturer = ManufacturerComboBox.SelectedItem?.ToString();

            List<Products> sortedAndFilteredProducts =
                SearchAndFilterProducts(searchText, selectedManufacturer);

            ProductViewModel productViewModel = (ProductViewModel)this.DataContext;
            productViewModel.Products = new ObservableCollection<Products>(DatabaseControl.SortingProducts("desc", sortedAndFilteredProducts));
            productViewModel.CurrentProductCount = DatabaseControl.GetCount(productViewModel.Products.ToList());
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductListBox.SelectedItem != null)
            {
                Products product = (Products)ProductListBox.SelectedItem;

                if (DatabaseControl.ValidProduct(product.Articul))
                {
                    ProductViewModel productViewModel = (ProductViewModel)this.DataContext;
                    productViewModel.Products.Remove(product);

                    DatabaseControl.DeleteProduct(product);

                    productViewModel.CurrentProductCount = DatabaseControl.GetCount(productViewModel.Products.ToList());
                    productViewModel.TotalProductCount = DatabaseControl.GetCount();
                    MessageBox.Show("Удаление товара успешно");
                }
                else
                {
                    MessageBox.Show("Данный товар присутствует в заказе");
                }

            }
            else
            {
                MessageBox.Show("Товар не выбран");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductListBox.SelectedItem != null)
            {
                Products product = (Products)ProductListBox.SelectedItem;
                mainWindow.OpenPage(MainWindow.Pages.EditProduct, product, user);
            }
            else
            {
                MessageBox.Show("Товар не выбран");
            }
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.Pages.AddProduct, user);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();

            string selectedManufacturer = ManufacturerComboBox.SelectedItem?.ToString();


            ProductViewModel productViewModel = (ProductViewModel)this.DataContext;
            productViewModel.Products = new ObservableCollection<Products>(SearchAndFilterProducts(searchText, selectedManufacturer));
            productViewModel.CurrentProductCount = DatabaseControl.GetCount(productViewModel.Products.ToList());
        }

        private void ManufacturerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();

            string selectedManufacturer = ManufacturerComboBox.SelectedItem?.ToString();

            ProductViewModel productViewModel = (ProductViewModel)this.DataContext;
            productViewModel.Products = new ObservableCollection<Products>(SearchAndFilterProducts(searchText, selectedManufacturer));
            productViewModel.CurrentProductCount = DatabaseControl.GetCount(productViewModel.Products.ToList());
        }

        private List<Products> SearchAndFilterProducts(string searchText, string selectedManufacturer)
        {
            List<Products> searchedProducts = SearchProducts(searchText);

            if (selectedManufacturer != null && selectedManufacturer != "Все производители")
            {
                searchedProducts = DatabaseControl.FilteredProducts(selectedManufacturer, searchedProducts);
            }
            return searchedProducts;
        }

        private List<Products> SearchProducts(string searchText)
        {
            List<Products> searchedProducts = new List<Products>();

            List<Products> products = DatabaseControl.GetProducts();

            foreach (Products product in products)
            {
                if (product.Product_name.ToLower().Contains(searchText) ||
                    product.Description.ToLower().Contains(searchText) ||
                    product.Manufacturer.ToLower().Contains(searchText))
                {
                    searchedProducts.Add(product);
                }
            }

            return searchedProducts;
        }

        private void CreateOrderClick(object sender, RoutedEventArgs e)
        {
            if (user.User_id != 0)
            {
                if (ProductListBox.SelectedItems.Count != 0)
                {
                    ObservableCollection<Products> selectedProducts = new ObservableCollection<Products>();
                    foreach (Products product in ProductListBox.SelectedItems)
                    {
                        selectedProducts.Add(product);
                    }

                    mainWindow.OpenPage(MainWindow.Pages.CreateOrder, user, selectedProducts);
                }
                else
                {
                    MessageBox.Show("Выберете товары из списка");
                }
            }
            else
            {
                MessageBox.Show("Чтобы создать заказ нужно войти в аккаунт");
            }
        }
        private void GoToOrdersClick(object sender, RoutedEventArgs e)
        {
            if (user.User_id != 0)
            {
                mainWindow.OpenPage(MainWindow.Pages.ViewOrders, user);
            }
            else
            {
                MessageBox.Show("Чтобы перейти к заказам нужно войти в аккаунт");
            }
        }
    }
}
