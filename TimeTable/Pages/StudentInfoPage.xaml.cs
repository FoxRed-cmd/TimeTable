using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeTable.Pages
{
    public partial class StudentInfoPage : Page
    {
        public Student Student { get; set; }
        public StudentInfoPage(Student student)
        {
            InitializeComponent();
            Student = student;
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
}
