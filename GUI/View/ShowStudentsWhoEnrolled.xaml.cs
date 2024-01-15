using CLI.Controller;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for ShowStudentsWhoEnrolled.xaml
    /// </summary>
    public partial class ShowStudentsWhoEnrolled : Window
    {
        private App app;
        private const string SRB = "sr-RS";
        private const string ENG = "en-US";
        private StudentController _studentController;
        private SubjectController _subjectController;
        public ObservableCollection<StudentDTO> StudentDtos { get; set; }
        private SubjectDTO SelectedSubject1 { get; set; }
        private SubjectDTO SelectedSubject2 { get; set; }

        public ShowStudentsWhoEnrolled(StudentController studentController, SubjectController subjectController, SubjectDTO selectedSubject1, SubjectDTO selectedSubject2)
        {
            InitializeComponent();
            DataContext = this;
            app = (App)Application.Current;
            SelectedSubject1 = selectedSubject1;
            SelectedSubject2 = selectedSubject2;
            StudentDtos = new ObservableCollection<StudentDTO>();

            _studentController = studentController;
            _subjectController = subjectController;

            Update();
        }
        public void Update()
        {
            StudentDtos.Clear();
            foreach(Student student in _studentController.GetAllStudents())
            {
                Subject? ss1 = _subjectController.GetSubjectById(SelectedSubject1.Id);
                Subject? ss2 = _subjectController.GetSubjectById(SelectedSubject2.Id);

                if(student.UnsubmittedSubjects.Contains(ss1) && student.UnsubmittedSubjects.Contains(ss2))
                {
                    StudentDtos.Add(new StudentDTO(student));
                }
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) && Keyboard.IsKeyDown(Key.E))
                MenuItem_Click_English(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftShift) && Keyboard.IsKeyDown(Key.R))
                MenuItem_Click_Serbian(sender, e);
        }

        private void MenuItem_Click_English(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(ENG);
        }

        private void MenuItem_Click_Serbian(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(SRB);
        }
    }
}
