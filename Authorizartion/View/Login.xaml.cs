using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                    Users Users = DatabaseControl.GetUser(TextboxLogin.Text, Password.Password);
                    if (Users is not null)
                    {
                        MessageBox.Show("Вход успешен");
                        mainwindow.OpenPage(MainWindow.Pages.MainApp, Users);
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
    }
}
