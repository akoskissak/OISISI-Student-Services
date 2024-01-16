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
    /// Interaction logic for SubjectsForDepartment.xaml
    /// </summary>
    public partial class SubjectsForDepartment : Window
    {
        private App app;
        private const string SRB = "sr-RS";
        private const string ENG = "en-US";

        private DepartmentController _departmentController;
        private SubjectController _subjectController;
        public ObservableCollection<SubjectDTO> SubjectForDepartmentDtos { get; set; }

        private DepartmentDTO SelectedDepartment { get; set; }
        public SubjectsForDepartment(DepartmentDTO selectedDepartment, DepartmentController departmentController)
        {
            InitializeComponent();
            DataContext = this;
            app = (App)Application.Current;

            this._departmentController = departmentController;
            this._subjectController = new SubjectController();
            this.SelectedDepartment = selectedDepartment;

            this.SubjectForDepartmentDtos = new ObservableCollection<SubjectDTO>();

            Update();
        }

        private void Update()
        {
            SubjectForDepartmentDtos.Clear();
            foreach(Professor professor in _departmentController.GetAllProfessorsByDepartmentId(SelectedDepartment.Id))
            {
                foreach(Subject subject in _subjectController.GetSubjectsByProfessorId(professor.Id))
                {
                    SubjectForDepartmentDtos.Add(new SubjectDTO(subject));
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.G))
                MenuItem_Click_English(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.R))
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
