using System.Windows;
using System.Windows.Controls;

namespace DishesCompany
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public MainWindow mainwindow;
        public Login(MainWindow mainwindow)
        {
            InitializeComponent();
            this.mainwindow = mainwindow;
        }
        private void EnterClick(object sender, RoutedEventArgs e)
        {
            if (TextboxLogin.Text != null)
            {
                if (Password.Password != null)
                {
                    Users User = DatabaseControl.GetUser(TextboxLogin.Text, Password.Password);
                    if (User is not null)
                    {
                        MessageBox.Show("Вход успешен");
                        mainwindow.OpenPage(MainWindow.Pages.MainApp, User);
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль, попробуйте заново");
                    }
                }
                else
                {
                    MessageBox.Show("Введите пароль");
                }
            }
            else
            {
                MessageBox.Show("Введите логин");
            }
        }

        private void ReginClick(object sender, RoutedEventArgs e)
        {
            mainwindow.OpenPage(MainWindow.Pages.Regin);
        }

        private void GuestClick(object sender, RoutedEventArgs e)
        {
            Users user = new Users();
            mainwindow.OpenPage(MainWindow.Pages.MainApp, user);
        }
    }
}
