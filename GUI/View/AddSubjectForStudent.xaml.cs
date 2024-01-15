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
    /// Interaction logic for AddSubjectForStudent.xaml
    /// </summary>
    public partial class AddSubjectForStudent : Window
    {
        private App app;
        private const string SRB = "sr-RS";
        private const string ENG = "en-US";
        private StudentDTO SelectedStudent { get; set; }
        public ObservableCollection<SubjectDTO> SubjectDtosWithoutStudent { get; set; }
        public ObservableCollection<SubjectDTO> SubjectDTOs { get; set; }

        private StudentController _studentController;
        private StudentSubjectController _studentSubjectController;
        private SubjectController _subjectController;

        public List<SubjectDTO> SelectedSubjectDto { get; set; }

        public AddSubjectForStudent(StudentController studentController, StudentDTO selectedStudent, ObservableCollection<SubjectDTO> sdtos)
        {
            InitializeComponent();
            DataContext = this;
            app = (App)Application.Current;
            this._studentController = studentController;
            this._studentSubjectController = new StudentSubjectController();
            this._subjectController = new SubjectController();

            this.SubjectDTOs = sdtos;
            SubjectDtosWithoutStudent = new ObservableCollection<SubjectDTO>();
            SelectedStudent = selectedStudent;

            Update();
        }

        public void Update()
        {
            SubjectDtosWithoutStudent.Clear();
            List<Subject> subjects = _studentSubjectController.GetAllSubjectsNotByStudent(SelectedStudent.Id);
            foreach (Subject subject in subjects)
            {
                SubjectDtosWithoutStudent.Add(new SubjectDTO(subject));
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedSubjectDto == null)
            {
                MessageBox.Show("Please choose a subject to add!");
            }
            else
            {
                foreach(SubjectDTO sdto in SelectedSubjectDto)
                {
                    Student student = _studentController.GetStudentById(SelectedStudent.Id);
                    StudentSubject studentSubject = new StudentSubject(SelectedStudent.Id, sdto.Id);
                    _studentSubjectController.AddStudentForSubject(student, sdto.Id);
                    _studentSubjectController.AddSubjectForStudent(sdto.Id, student.Id);
                    _studentSubjectController.AddStudentSubject(studentSubject);
                    SubjectDTOs.Add(sdto);
                }
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SubjectsToAddForProfessorDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedSubjectDto = SubjectsToAddForProfessorDataGrid.SelectedItems.Cast<SubjectDTO>().ToList();
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
