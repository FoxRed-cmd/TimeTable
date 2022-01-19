using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeTable.DialogWindows;

namespace TimeTable.Pages
{
    public partial class UsersPage : Page
    {
        private UsersEditWindow editWindow;
        public UsersPage()
        {
            InitializeComponent();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            dataGridUsers.ItemsSource = User.GetAllDataFromTable();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            editWindow = new UsersEditWindow() { UsersPageModel = this };
            editWindow.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            User user = dataGridUsers.SelectedItem as User;
            if (user != null)
            {
                editWindow = new UsersEditWindow(user) { UsersPageModel = this };
                editWindow.ShowDialog();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var users = dataGridUsers.SelectedItems;
            if (users.Count != 0)
            {
                if (DialogResult.OK == System.Windows.Forms.MessageBox.Show("Внимание! Выбранные объекты будут удалены! Вы уверены?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    try
                    {
                        foreach (var item in users)
                        {
                            User user = item as User;
                            User.DeleteUserByLogin(user.Login);
                        }
                        dataGridUsers.ItemsSource = User.GetAllDataFromTable();
                    }
                    catch (System.Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
