using Microsoft.Win32;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DishesCompany
{
    /// <summary>
    /// Логика взаимодействия для EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Page
    {
        public MainWindow mainWindow;
        private Users user;
        private const string imageSource = "C:/Users/malac/OneDrive/Документы/VisualStudioLabs/source/repos/Authorizartion/Authorizartion/Resources/Images/";
        private OpenFileDialog img;
        private Products tempProduct;
        public EditProduct(MainWindow mainWindow, Products product, Users user)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            this.DataContext = product;
            this.user = user;
            tempProduct = product;
            FullName.Text = user.Full_name;
        }
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.Pages.Login);
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.Pages.MainApp, user);
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TextBoxAmount.Text, out int productAmount) || !decimal.TryParse(TextBoxProductCost.Text, CultureInfo.InvariantCulture, out decimal productCost))
            {
                MessageBox.Show("Пожалуйства, введите числа в корректном формате");
                return;
            }
            tempProduct.Articul = TextBoxArticul.Text;
            tempProduct.Product_name = TextBoxProductName.Text;
            tempProduct.Product_type = TextBoxProductType.Text;
            tempProduct.Amount = productAmount;
            tempProduct.Measurement_unit = TextBoxMeasurementUnit.Text;
            tempProduct.Manufacturer = TextBoxManufacturer.Text;
            tempProduct.Supplier = TextBoxSupplier.Text;
            tempProduct.Product_cost = productCost;
            tempProduct.Description = TextBoxDescription.Text;

            if (img != null)
            {
                File.Delete($"{imageSource}{tempProduct.Image}");
                File.Copy(img.FileName, Path.Combine(imageSource, $"{TextBoxArticul.Text}{Path.GetExtension(img.SafeFileName)}"), true);
                tempProduct.Image = $"{TextBoxArticul.Text}{Path.GetExtension(img.SafeFileName)}";
            }

            DatabaseControl.EditProductRecord(tempProduct);
            MessageBox.Show("Данные о товаре изменены");
            mainWindow.OpenPage(MainWindow.Pages.MainApp, user);
        }
        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.jpg, *.png)|*.jpg;*.png;*.JPG;*.PNG";
            if (openFileDialog.ShowDialog() == true)
            {
                img = openFileDialog;
            }
        }

    }
}
