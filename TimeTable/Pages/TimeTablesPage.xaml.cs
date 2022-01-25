using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using TimeTable.DialogWindows;

namespace TimeTable.Pages
{
    public partial class TimeTablesPage : Page
    {
        private TimeTablesEditWindow editWindow;
        public TimeTablesPage()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            editWindow = new TimeTablesEditWindow(this.DataContext as MainDataViewModel);
            editWindow.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            TimeTableModel timeTableModel = dataGridTimeTables.SelectedItem as TimeTableModel;
            if (timeTableModel != null)
            {
                editWindow = new TimeTablesEditWindow(timeTableModel, this.DataContext as MainDataViewModel);
                editWindow.ShowDialog();
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var timeTables = dataGridTimeTables.SelectedItems;
            if (timeTables.Count != 0)
            {
                if (DialogResult.OK == System.Windows.Forms.MessageBox.Show("Выбранные объекты будут удалены! Вы уверены?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    try
                    {
                        foreach (var item in timeTables)
                        {
                            TimeTableModel timeTable = item as TimeTableModel;
                            TimeTableModel.DeleteTimeTable(timeTable.Id);
                        }
                        var context = this.DataContext as MainDataViewModel;
                        context.TimeTableModels = TimeTableModel.GetAllDataFromTable().ToList();
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
