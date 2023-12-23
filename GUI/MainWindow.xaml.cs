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

        private ProfessorController professorController {  get; set; }
        private StudentController studentController { get; set; }    
        private SubjectController subjectController { get; set; }

        public static RoutedCommand OpenAddEntityCommand = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ProfessorDtos = new ObservableCollection<ProfessorDTO>();
            StudentDtos = new ObservableCollection<StudentDTO>();
            SubjectDtos = new ObservableCollection<SubjectDTO>();

            SetInitialWindowSize();

            studentController = new StudentController();
            professorController = new ProfessorController();
            subjectController = new SubjectController();

            studentController.Subscribe(this);
            professorController.Subscribe(this);
            subjectController.Subscribe(this);

            // i ovo
            ///
            ////
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            Update();
        }

        private void SetInitialWindowSize()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            double targetWidth = screenWidth * 0.75;
            double targetHeight = screenHeight * 0.75;

            Width = targetWidth;
            Height = targetHeight;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if (ti != null && ti.Name != null && ti.Name == "ProfessorsTab")
            {
                AddProfessor addProfessorWindow = new AddProfessor(professorController, Left, Top, Width, Height);  //i ovo treba izmeniti
                addProfessorWindow.ShowDialog();
            }
            else if(ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                AddStudent addStudent = new AddStudent(studentController);
                addStudent.ShowDialog();
            }
            else if(ti != null && ti.Name != null && ti.Name == "SubjectsTab")
            {
                AddSubject addSubject = new AddSubject(subjectController, Left, Top, Width, Height);        // i ovo treba izmeniti
                addSubject.ShowDialog();
            }
            
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if (ti != null && ti.Name != null && ti.Name == "ProfessorsTab")
            {
                if (SelectedProfessor != null)
                {
                    UpdateProfessor updateProfessorWindow = new UpdateProfessor(professorController, SelectedProfessor);
                    updateProfessorWindow.ShowDialog();
                }
                else
                    MessageBox.Show("Please choose a professor to update!");
            }
            else if(ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                if (SelectedStudent != null)
                {
                    UpdateStudent updateStudentWindow = new UpdateStudent(studentController, SelectedStudent);
                    updateStudentWindow.ShowDialog();
                }
                else
                    MessageBox.Show("Please choose a student to update!");
            }
            else if(ti != null && ti.Name != null && ti.Name == "SubjectsTab")
            {
                if (SelectedSubject != null)
                {
                    UpdateSubject updateSubjectWindow = new UpdateSubject(subjectController, SelectedSubject);
                    updateSubjectWindow.ShowDialog();
                }
                else
                    MessageBox.Show("Please choose a subject to update!");
            }
        }

        public void Update()
        {
            ProfessorDtos.Clear();
            foreach (Professor professor in professorController.GetAllProfessors())
                ProfessorDtos.Add(new ProfessorDTO(professor));

            StudentDtos.Clear();
            foreach(Student student in studentController.GetAllStudents())
                StudentDtos.Add(new StudentDTO(student));

            SubjectDtos.Clear();
            foreach (Subject subject in subjectController.GetAllSubjects())
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
                {
                    string messageBoxText = "Are you sure you want to delete professor?";
                    string caption = "Delete professor";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);
                    if (result == MessageBoxResult.Yes)
                    {
                        professorController.RemoveProfessor(SelectedProfessor.Id);
                        ProfessorDtos.Remove(SelectedProfessor);
                        MessageBox.Show("Professor deleted successfully.", "Deletion Successful", MessageBoxButton.OK);
                    }

                }
            }
            else if (ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                if (SelectedStudent == null)
                    MessageBox.Show("Please choose a student to delete!");
                else
                {
                    string messageBoxText = "Are you sure you want to delete student?";
                    string caption = "Delete student";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);
                    if (result == MessageBoxResult.Yes)
                    {
                        studentController.RemoveStudent(SelectedStudent.Id);
                        StudentDtos.Remove(SelectedStudent);
                        MessageBox.Show("Student deleted successfully.", "Deletion Successful", MessageBoxButton.OK);
                    }
                }
            }
            else if (ti != null && ti.Name != null && ti.Name == "SubjectsTab")
            {
                if (SelectedSubject == null)
                {
                    MessageBox.Show("Please choose a subject to delete!");
                }
                else
                {
                    string messageBoxText = "Are you sure you want to delete subject?";
                    string caption = "Delete subject";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);
                    if(result == MessageBoxResult.Yes)
                    {
                        subjectController.RemoveSubject(SelectedSubject.Id);
                        SubjectDtos.Remove(SelectedSubject);
                        MessageBox.Show("Subject deleted successfully.", "Deletion Successful", MessageBoxButton.OK);
                    }
                }
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.A))
                Add_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.U))
                Update_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D))
                Delete_Click(sender, e);
        }

        
    }
}
