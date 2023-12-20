using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using GUI.DTO;
using GUI.View;
using System.Windows.Threading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CLI.Controller;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        public ObservableCollection<ProfessorDTO> ProfessorDtos { get; set; }
        public ObservableCollection<SubjectDTO> SubjectDtos { get; set; }
        public ObservableCollection<StudentDTO> StudentDtos { get; set; }
        
        public ProfessorDTO SelectedProfessor { get; set; }

        public SubjectDTO SelectedSubject { get; set; }
        
        public StudentDTO SelectedStudent { get; set; }
        

        private ProfessorDAO _professorDao;     //ovo treba brisati
        private SubjectDAO _subjectDao;         // i ovo i staviti private controller
        private StudentController studentController { get; set; }         

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ProfessorDtos = new ObservableCollection<ProfessorDTO>();
            //i ovo treba izmeniti
            _professorDao = new ProfessorDAO();
            _professorDao.ProfessorObservable.Subscribe(this);
            //
            StudentDtos = new ObservableCollection<StudentDTO>();
            studentController = new StudentController();
            studentController.Subscribe(this);

            SubjectDtos = new ObservableCollection<SubjectDTO>();
            // i ovo
            _subjectDao = new SubjectDAO();
            _subjectDao.SubjectObservable.Subscribe(this);
            ///
            ////
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            ////

            Update();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if (ti != null && ti.Name != null && ti.Name == "ProfessorsTab")
            {
                AddProfessor addProfessorWindow = new AddProfessor(_professorDao);  //i ovo treba izmeniti
                addProfessorWindow.Show();
            }
            else if(ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                AddStudent addStudent = new AddStudent(studentController);
                addStudent.Show();
            }
            else if(ti != null && ti.Name != null && ti.Name == "SubjectsTab")
            {
                AddSubject addSubject = new AddSubject(_subjectDao);        // i ovo treba izmeniti
                addSubject.Show();
            }
            
        }

        
        //private void ClickAddProfessor(object sender, RoutedEventArgs e)
        //{
        //    AddProfessor addProfessorWindow = new AddProfessor(_professorDao);
        //    addProfessorWindow.Show();
        //}
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if (ti != null && ti.Name != null && ti.Name == "ProfessorsTab")
            {
                if (SelectedProfessor != null)
                {
                    UpdateProfessor updateProfessorWindow = new UpdateProfessor(_professorDao, SelectedProfessor);
                    updateProfessorWindow.Show();
                }
                else
                    MessageBox.Show("Please choose a professor to update!");
            }
            else if(ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                if (SelectedStudent != null)
                {
                    UpdateStudent updateStudentWindow = new UpdateStudent(studentController, SelectedStudent);
                    updateStudentWindow.Show();
                }
                else
                    MessageBox.Show("Please choose a student to update!");
            }
            else if(ti != null && ti.Name != null && ti.Name == "SubjectsTab")
            {
                if (SelectedSubject != null)
                {
                    UpdateSubject updateSubjectWindow = new UpdateSubject(_subjectDao, SelectedSubject);
                    updateSubjectWindow.Show();
                }
                else
                    MessageBox.Show("Please choose a subject to update!");
            }
        }

        public void Update()
        {
            ProfessorDtos.Clear();
            foreach (Professor professor in _professorDao.GetAllProfessors())
                ProfessorDtos.Add(new ProfessorDTO(professor));

            StudentDtos.Clear();
            foreach(Student student in studentController.GetAllStudents())
                StudentDtos.Add(new StudentDTO(student));

            SubjectDtos.Clear();
            foreach (Subject subject in _subjectDao.GetAllSubjects())
                SubjectDtos.Add(new SubjectDTO(subject));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if (ti != null && ti.Name != null && ti.Name == "ProfessorsTab")
            {
                if (SelectedProfessor == null)
                    MessageBox.Show("Please choose a professor to delete!");
                else
                    _professorDao.RemoveProfessor(SelectedProfessor.Id);
            }
            else if(ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                if (SelectedStudent == null)
                    MessageBox.Show("Please choose a student to delete!");
                else
                    studentController.RemoveStudent(SelectedStudent.Id);
            }
            else if (ti != null && ti.Name != null && ti.Name == "SubjectsTab")
            {
                if (SelectedSubject == null)
                    MessageBox.Show("Please choose a subject to delete!");
                else
                    _subjectDao.RemoveSubject(SelectedSubject.Id);
            }
        }

        private void ProfessorsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedProfessor = ProfessorsDataGrid.SelectedItem as ProfessorDTO;
        }

        private void SubjectDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedSubject = SubjectsDataGrid.SelectedItem as SubjectDTO;
        }
        private void StudentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedStudent = StudentsDataGrid.SelectedItem as StudentDTO;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            DisplayDateTextBlock.Text = DateTime.Now.ToString();
        }

    }
}
