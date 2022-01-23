using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace TimeTable.Pages
{
    public partial class StudentInfoPage : Page
    {
        private byte[] photo;
        private Student student;
        public Student Student { get; set; }
        public StudentInfoPage(Student student)
        {
            InitializeComponent();
            Student = student;
            LoadingStudent();
        }
        private void LoadingStudent()
        {
            if (Student != null)
            {
                txtLogin.Text = Student.Login;
                txtPass.Text = User.GetUserByLogin(Student.Login).Password;
                txtName.Text = Student.Name;
                txtGroupe.Text = Student.Group;
                txtPhone.Text = Student.Phone;
                txtEmail.Text = Student.Email;
                if (Student.Photo == null)
                    Photo.Source = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.blank_profile_picture_973460_960_720.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                else
                {
                    using (Stream stream = new MemoryStream(Student.Photo))
                    {
                        Bitmap bitmap = new Bitmap(stream);
                        Photo.Source = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    }
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
                    LoadingStudent();
                }
                catch (Exception)
                {

                }
            };
        }
    }
}
