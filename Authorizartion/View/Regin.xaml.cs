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
    /// Логика взаимодействия для Regin.xaml
    /// </summary>
    public partial class Regin : Page
    {
        public MainWindow mainwindow;
        public Regin(MainWindow mainwindow)
        {
            InitializeComponent();
            this.mainwindow = mainwindow;
        }        
        private void RegClick(object sender, RoutedEventArgs e)
        {
            bool userExists = DatabaseControl.CheckUser(TextboxLogin.Text);
            if (!userExists)
            {
                string[] dataLogin = TextboxLogin.Text.Split('@');
                if (dataLogin.Length == 2)
                {
                    string[] data2Login = dataLogin[1].Split('.');
                    if (data2Login.Length == 2)
                    {
                        if (TextboxLogin.Text != null)
                        {
                            if (Password.Password.Length >= 6 && Password.Password.Length <= 30)
                            {
                                if (RePassword.Password != null)
                                {
                                    if (RePassword.Password == Password.Password)
                                    {
                                        MessageBox.Show("Регистрация успешна");
                                        DatabaseControl.AddUserRecord(
                                            $"{TextboxName.Text} {TextboxSurname.Text} {TextboxLastName.Text}"
                                            ,TextboxLogin.Text, Password.Password);
                                        mainwindow.OpenPage(MainWindow.Pages.Login);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Пароли не совпадают");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Введите пароль повторно");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Пароль введен неправильно\n" +
                                    "Пароль содержит минимум 6 символов");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введите логин");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Логин введен неправильно");
                    }
                }
                else
                {
                    MessageBox.Show("Логин введен неправильно");
                }
            }
            else
            {
                MessageBox.Show("Такой логин уже использован");
            }
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            mainwindow.OpenPage(MainWindow.Pages.Login);
        }
    }
}
