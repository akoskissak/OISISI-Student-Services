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
    /// Interaction logic for StudentsForProfessor.xaml
    /// </summary>
    public partial class StudentsForProfessor : Window
    {
        private App app;
        private const string SRB = "sr-RS";
        private const string ENG = "en-US";

        private StudentSubjectController _studentSubjectController;
        private ProfessorSubjectController _professorSubjectController;
        private StudentController _studentController;

        public StudentsForProfessorDTO StudentsForProfessorDto;

        public ObservableCollection<StudentsForProfessorDTO> StudentsForProfessorDtos { get; set; }
        private int profId;

        public StudentsForProfessor(int professorId)
        {
            InitializeComponent();
            DataContext = this;
            app = (App)Application.Current;

            this._studentSubjectController = new StudentSubjectController();
            this._professorSubjectController = new ProfessorSubjectController();
            this._studentController = new StudentController();

            StudentsForProfessorDtos = new ObservableCollection<StudentsForProfessorDTO>();

            profId = professorId;
            show();
        }

        public void show()
        {
            StudentsForProfessorDtos.Clear();
            List<Subject> subjects = _professorSubjectController.GetAllSubjectsByProfessorId(profId);
            if (subjects.Count > 0)
            {
                List<Student>? studentslist = _studentSubjectController.GetAllStudentsOnSubjects(subjects);
                if (studentslist != null)
                {
                    foreach (Student s in studentslist)
                    {
                        StudentsForProfessorDtos.Add(new StudentsForProfessorDTO(s));
                    }
                }

            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if(searchTextBox.Text.Length != 0)
            {
                StudentsForProfessorDtos.Clear();
                List<Student>? students = _studentController.GetStudentByText(searchTextBox.Text);
                if(students != null)
                {
                    foreach(Student student in students)
                    {
                        StudentsForProfessorDtos.Add(new StudentsForProfessorDTO(student));
                    }
                }
            }
            else
            {
                show();
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
