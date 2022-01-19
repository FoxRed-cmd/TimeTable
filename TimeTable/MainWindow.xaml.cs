using System;
using System.Collections.Generic;
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
using Microsoft.Data.Sqlite;

namespace TimeTable
{
    public partial class MainWindow : Window
    {
        List<User>? users = null;
        MainDataWindow? mainDataWindow = null;
        StudentMainWindow? studentMainWindow = null;
        public MainWindow()
        {
            InitializeComponent();
            txtLogin.GotFocus += (s, e) =>
            {
                if (txtLogin.Text == "Login")
                    txtLogin.Text = string.Empty;
            };
            txtLogin.LostFocus += (s, e) =>
            {
                if (txtLogin.Text == string.Empty)
                    txtLogin.Text = "Login";
            };
            txtPW.GotFocus += (s, e) =>
            {
                if (txtPW.Password == "Password")
                    txtPW.Password = string.Empty;
            };
            txtPW.LostFocus += (s, e) =>
            {
                if (txtPW.Password == string.Empty)
                    txtPW.Password = "Password";
            };
            KeyDown += (s, e) =>
            {
                if (e.Key == Key.Enter)
                    ConnectAndEnter();
            };
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void ExitButton_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void EnterButton_Click(object sender, RoutedEventArgs e) => ConnectAndEnter();

        private void ConnectAndEnter()
        {
            users = User.GetAllDataFromTable().ToList();
            User? user = users?.FirstOrDefault(u => u.Login == txtLogin.Text && u.Password == txtPW.Password);
            if (user != null)
            {
                if (user.Status.ToLower() == "admin")
                {
                    mainDataWindow = new MainDataWindow(user) { Owner = this };
                    mainDataWindow.Show();
                    this.Hide();
                }
                else
                {
                    studentMainWindow = new StudentMainWindow(user) { Owner = this };
                    studentMainWindow.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
