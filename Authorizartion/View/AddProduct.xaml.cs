using Microsoft.Win32;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DishesCompany
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Page
    {
        public MainWindow mainWindow;
        private Users user;
        private const string imageSource = "C:/Users/malac/OneDrive/Документы/VisualStudioLabs/source/repos/Authorizartion/Authorizartion/Resources/Images/";
        private OpenFileDialog img;
        public AddProduct(MainWindow mainwindow, Users user)
        {
            InitializeComponent();
            this.mainWindow = mainwindow;
            this.user = user;
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

        private void AddProductClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxArticul.Text) ||
                string.IsNullOrEmpty(TextBoxProductName.Text) ||
                string.IsNullOrEmpty(TextBoxProductType.Text) ||
                string.IsNullOrEmpty(TextBoxAmount.Text) ||
                string.IsNullOrEmpty(TextBoxMeasurementUnit.Text) ||
                string.IsNullOrEmpty(TextBoxManufacturer.Text) ||
                string.IsNullOrEmpty(TextBoxSupplier.Text) ||
                string.IsNullOrEmpty(TextBoxProductCost.Text)
                )
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return;
            }
            if (!int.TryParse(TextBoxAmount.Text, out int productAmount) || !decimal.TryParse(TextBoxProductCost.Text, CultureInfo.InvariantCulture, out decimal productCost))
            {
                MessageBox.Show("Пожалуйста, введите числа в корректном формате");
                return;
            }
            if (img == null)
            {
                MessageBox.Show("Пожалуйста, выберите изображение");
                return;
            }
            string filePath = Path.Combine(imageSource, $"{TextBoxArticul.Text}{Path.GetExtension(img.SafeFileName)}");
            File.Copy(img.FileName, filePath, true);
            MessageBox.Show("Изображение успешно добавлено");

            Products product = new Products(TextBoxArticul.Text, TextBoxProductName.Text, TextBoxProductType.Text, productAmount, TextBoxMeasurementUnit.Text,
                TextBoxManufacturer.Text, TextBoxSupplier.Text, productCost, TextBoxDescription.Text, $"{TextBoxArticul.Text}{Path.GetExtension(filePath)}");

            DatabaseControl.AddProductRecord(product);

            MessageBox.Show("Товар успешно добавлен");
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
