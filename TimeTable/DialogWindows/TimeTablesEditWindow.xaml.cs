using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace TimeTable.DialogWindows
{
    public partial class TimeTablesEditWindow : Window
    {
        private TimeTableModel timeTableModel;
        private string currentTimeTable;
        private MainDataViewModel viewModel;
        public TimeTablesEditWindow(MainDataViewModel viewModel)
        {
            InitializeComponent();
            txtTitle.Text = "Добавить";
            this.viewModel = viewModel;
        }
        public TimeTablesEditWindow(TimeTableModel timeTableModel, MainDataViewModel viewModel)
        {
            InitializeComponent();
            txtTitle.Text = "Редактировать";
            currentTimeTable = timeTableModel.Id;
            txtID.Text = timeTableModel.Id;
            txtSub.Text = timeTableModel.Subject;
            txtGroup.Text = timeTableModel.Group;
            txtDay.Text = timeTableModel.DayOfWeek;
            txtTime.Text = timeTableModel.Time;
            this.viewModel = viewModel;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtTitle.Text == "Добавить")
            {
                if (txtSub.Text != "" && txtGroup.Text != "" && txtDay.Text != "" && txtTime.Text != "")
                {
                    try
                    {
                        timeTableModel = new TimeTableModel()
                        {
                            Id = txtID.Text,
                            Subject = txtSub.Text,
                            Group = txtGroup.Text,
                            DayOfWeek = txtDay.Text,
                            Time = txtTime.Text,
                        };
                        TimeTableModel.AddTimeTable(timeTableModel);
                        txtID.Clear();
                        viewModel.TimeTableModels = TimeTableModel.GetAllDataFromTable().ToList();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                    System.Windows.MessageBox.Show("Поля: Предмет, Группа, День недели и Время - обязательны к заполнению", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (txtID.Text != "" && txtSub.Text != "" && txtGroup.Text != "" && txtDay.Text != "" && txtTime.Text != "")
                {
                    try
                    {
                        timeTableModel = new TimeTableModel()
                        {
                            Id = txtID.Text,
                            Subject = txtSub.Text,
                            Group = txtGroup.Text,
                            DayOfWeek = txtDay.Text,
                            Time = txtTime.Text,
                        };
                        TimeTableModel.UpdateTimeTable(timeTableModel, currentTimeTable);
                        viewModel.TimeTableModels = TimeTableModel.GetAllDataFromTable().ToList();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                    System.Windows.MessageBox.Show("Поля: Предмет, Группа, День недели и Время - обязательны к заполнению", "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
