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
                foreach (Student s in _studentSubjectController.GetAllStudentsOnSubjects(subjects))
                {
                    StudentsForProfessorDtos.Add(new StudentsForProfessorDTO(s));
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
    }
}
