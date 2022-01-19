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
            editWindow = new TimeTablesEditWindow() { TimeTablesPage = this };
            editWindow.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            TimeTableModel timeTableModel = dataGridTimeTables.SelectedItem as TimeTableModel;
            if (timeTableModel != null)
            {
                editWindow = new TimeTablesEditWindow(timeTableModel) { TimeTablesPage = this };
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
                        dataGridTimeTables.ItemsSource = TimeTableModel.GetAllDataFromTable();
                    }
                    catch (System.Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            dataGridTimeTables.ItemsSource = TimeTableModel.GetAllDataFromTable();
        }
    }
}
