using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using GUI.DTO;
using GUI.View;
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

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        public ObservableCollection<ProfessorDTO> ProfessorDtos { get; set; }
        public ProfessorDTO SelectedProfessor { get; set; }
        private ProfessorDAO _professorDao { get; set; }

        public ObservableCollection<StudentDTO> StudentDtos { get; set; }
        public StudentDTO SelectedStudent { get; set; }
        private StudentDAO _studentDao { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ProfessorDtos = new ObservableCollection<ProfessorDTO>();
            _professorDao = new ProfessorDAO();
            _professorDao.ProfessorObservable.Subscribe(this);

            StudentDtos = new ObservableCollection<StudentDTO>();
            _studentDao = new StudentDAO();
            _studentDao.StudentObservable.Subscribe(this);

            Update();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if (ti != null && ti.Name != null && ti.Name == "ProfessorsTab")
            {
                AddProfessor addProfessor = new AddProfessor(_professorDao);
                addProfessor.Show();
            }

            if(ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                AddStudent addStudent = new AddStudent(_studentDao);
                addStudent.Show();
            }
        }

        
        private void ClickAddProfessor(object sender, RoutedEventArgs e)
        {
            AddProfessor addProfessorWindow = new AddProfessor(_professorDao);
            addProfessorWindow.Show();
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        public void Update()
        {
            ProfessorDtos.Clear();
            foreach (Professor professor in _professorDao.GetAllProfessors())
                ProfessorDtos.Add(new ProfessorDTO(professor));

            StudentDtos.Clear();
            foreach(Student student in _studentDao.GetAllStudents())
                StudentDtos.Add(new StudentDTO(student));
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

            if(ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                if (SelectedStudent == null)
                    MessageBox.Show("Please choose a student to delete!");
                else
                    _studentDao.RemoveStudent(SelectedStudent.Id);
            }
        }
    }
}
