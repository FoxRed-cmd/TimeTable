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
using System.Windows.Shapes;
using TimeTable.Pages;

namespace TimeTable.DialogWindows
{
    public partial class TimeTablesEditWindow : Window
    {
        private TimeTableModel timeTableModel;
        private string currentTimeTable;
        public TimeTablesPage TimeTablesPage { get; set; }
        public TimeTablesEditWindow()
        {
            InitializeComponent();
            txtTitle.Text = "Добавить";
        }
        public TimeTablesEditWindow(TimeTableModel timeTableModel)
        {
            InitializeComponent();
            txtTitle.Text = "Редактировать";
            currentTimeTable = timeTableModel.Id;
            txtID.Text = timeTableModel.Id;
            txtSub.Text = timeTableModel.Subject;
            txtGroup.Text = timeTableModel.Group;
            txtDay.Text = timeTableModel.DayOfWeek;
            txtTime.Text = timeTableModel.Time;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtTitle.Text == "Добавить")
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
                        TimeTableModel.AddTimeTable(timeTableModel);
                        txtID.Clear();
                        txtDay.Clear();
                        txtTime.Clear();
                        TimeTablesPage.dataGridTimeTables.ItemsSource = TimeTableModel.GetAllDataFromTable();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                    System.Windows.MessageBox.Show("Поля: Логин, Код группы и ФИО - обязательны к заполнению", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        TimeTablesPage.dataGridTimeTables.ItemsSource = TimeTableModel.GetAllDataFromTable();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                    System.Windows.MessageBox.Show("Поля: Логин, Код группы и ФИО - обязательны к заполнению", "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
