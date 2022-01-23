using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using TimeTable.DialogWindows;

namespace TimeTable.Pages
{
    public partial class StudentInfoPage : Page
    {
        private byte[] photo;
        private EditInfoForStudentWindow editInfoForStudentWindow;
        private Student student;
        private StudentMainWindow studentMainWindow;
        public Student Student { get; set; }
        public StudentInfoPage(Student student, Window owner)
        {
            InitializeComponent();
            Student = student;
            studentMainWindow = owner as StudentMainWindow;
            LoadingStudent(Student);
        }
        internal void LoadingStudent(Student student)
        {
            Student = student;
            if (Student != null)
            {
                txtLogin.Text = Student.Login;
                txtPass.Text = User.GetUserByLogin(Student.Login).Password;
                txtName.Text = Student.Name;
                txtGroupe.Text = Student.Group;
                txtPhone.Text = Student.Phone;
                txtEmail.Text = Student.Email;
                if (Student.Photo == null)
                    Photo.Source = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.Unkownman.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                else
                {
                    using (Stream stream = new MemoryStream(Student.Photo))
                    {
                        Bitmap bitmap = new Bitmap(stream);
                        Photo.Source = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    }
                }
                if (studentMainWindow.login != Student.Login)
                {
                    studentMainWindow.login = Student.Login;
                    studentMainWindow.boxUser.Text = $"Пользователь: {Student.Login}";
                }
                    
                 
            }
        }

        private void BtnEditPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Title = "Выберите фото",
                Filter = "png files (*.png)|*.png|jpg files (*.jpg)|*.jpg",
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            };
            if (System.Windows.Forms.DialogResult.OK == openFile.ShowDialog())
            {
                try
                {
                    photo = File.ReadAllBytes(openFile.FileName);
                    student = new Student()
                    {
                        Login = Student.Login,
                        Name = Student.Name,
                        Group = Student.Group,
                        Phone = Student.Phone,
                        Email = Student.Email,
                        Photo = photo,
                    };
                    Student.UpdateStudent(student, Student.Login);
                    Student = Student.GetStudentByLogin(student.Login);
                    LoadingStudent(Student);
                }
                catch (Exception)
                {

                }
            };
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            editInfoForStudentWindow = new EditInfoForStudentWindow(Student, this);
            editInfoForStudentWindow.ShowDialog();
        }
    }
}
