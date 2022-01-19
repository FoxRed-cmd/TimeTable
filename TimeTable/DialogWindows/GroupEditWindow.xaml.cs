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
    public partial class GroupEditWindow : Window
    {
        private Group group;
        private string currentGroup;
        public GroupsPage GroupsPage { get; set; }
        public GroupEditWindow()
        {
            InitializeComponent();
            txtTitle.Text = "Добавить";
        }

        public GroupEditWindow(Group group)
        {
            InitializeComponent();
            txtTitle.Text = "Редактировать";
            currentGroup = group.Name;
            txtGroup.IsEnabled = false;
            txtGroup.Text = group.Name;
            txtDesc.Text = group.Description;
            txtTime.Text = group.TrainingPeriod;
            txtForm.Text = group.FormOfStudy;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtTitle.Text == "Добавить")
            {
                if (txtGroup.Text != "" && txtTime.Text != "" && txtForm.Text != "")
                {
                    try
                    {
                        group = new Group()
                        {
                            Name = txtGroup.Text,
                            Description = txtDesc.Text,
                            TrainingPeriod = txtTime.Text,
                            FormOfStudy = txtForm.Text,
                        };
                        Group.AddGroup(group);
                        txtGroup.Clear();
                        txtDesc.Clear();
                        txtTime.Clear();
                        txtForm.Clear();
                        GroupsPage.dataGridGroups.ItemsSource = Group.GetAllDataFromTable();
                        
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                    System.Windows.MessageBox.Show("Поля: Группа, Время обучения и Форма обучения - обязательны к заполнению", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (txtGroup.Text != "" && txtTime.Text != "" && txtForm.Text != "")
                {
                    try
                    {
                        group = new Group()
                        {
                            Name = txtGroup.Text,
                            Description = txtDesc.Text,
                            TrainingPeriod = txtTime.Text,
                            FormOfStudy = txtForm.Text,
                        };
                        Group.UpdateGroup(group, currentGroup);
                        GroupsPage.dataGridGroups.ItemsSource = Group.GetAllDataFromTable();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                    System.Windows.MessageBox.Show("Поля: Группа, Время обучения и Форма обучения - обязательны к заполнению", "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
