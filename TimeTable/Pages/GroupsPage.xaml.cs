using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using TimeTable.DialogWindows;

namespace TimeTable.Pages
{
    public partial class GroupsPage : Page
    {
        public GroupsPage()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            GroupEditWindow groupEditWindow = new(this.DataContext as MainDataViewModel);
            groupEditWindow.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Group group = dataGridGroups.SelectedItem as Group;
            if (group != null)
            {
                GroupEditWindow groupEditWindow = new(group, this.DataContext as MainDataViewModel);
                groupEditWindow.ShowDialog();
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var groups = dataGridGroups.SelectedItems;
            if (groups.Count != 0)
            {
                if (DialogResult.OK == System.Windows.Forms.MessageBox.Show("Внимание! При удалении группы студенты и расписание этой группы также будут удалены! Вы уверены?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    try
                    {
                        foreach (var item in groups)
                        {
                            Group group = item as Group;
                            Student.DeleteStudentByGroup(group.Name);
                            TimeTableModel.DeleteTimeTableByGroup(group.Name);
                            Group.DeleteGroupByName(group.Name);
                        }
                        var context = this.DataContext as MainDataViewModel;
                        context.Groups = Group.GetAllDataFromTable().ToList();
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
