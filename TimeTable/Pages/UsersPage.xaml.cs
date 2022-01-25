using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            editWindow = new UsersEditWindow(this.DataContext as MainDataViewModel) { UsersPageModel = this };
            editWindow.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            User? user = dataGridUsers.SelectedItem as User;
            if (user != null)
            {
                editWindow = new UsersEditWindow(user, this.DataContext as MainDataViewModel) { UsersPageModel = this };
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
                        var context = this.DataContext as MainDataViewModel;
                        context.Users = User.GetAllDataFromTable().ToList();
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
