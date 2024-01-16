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
    /// Interaction logic for StudentConditionSubject.xaml
    /// </summary>
    public partial class StudentConditionSubject : Window
    {
        private App app;
        private const string SRB = "sr-RS";
        private const string ENG = "en-US";
        private StudentController _studentController;
        private SubjectController _subjectController;
        public SubjectDTO SelectedSubjectBefore { get; set; }
        public SubjectDTO SelectedSubjectNow { get; set; }
        public ObservableCollection<SubjectDTO> SubjectDtos { get; set; }

        public StudentConditionSubject(StudentController studentController, SubjectController subjectController, SubjectDTO selectedSubject)
        {
            InitializeComponent();
            DataContext = this;
            app = (App)Application.Current;
            this.SelectedSubjectBefore = selectedSubject;
            this._studentController = studentController;
            this._subjectController = subjectController;

            SubjectDtos = new ObservableCollection<SubjectDTO>();

            Update();
        }
        public void Update()
        {
            SubjectDtos.Clear();
            foreach(Subject subject in _subjectController.GetAllSubjects())
            {
                if(subject.Id != SelectedSubjectBefore.Id)
                    SubjectDtos.Add(new SubjectDTO(subject));
            }
        }

        private void ShowStudents1_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedSubjectNow != null)
            {
                ShowStudentsWhoEnrolled showStudentsWhoEnrolled = new ShowStudentsWhoEnrolled(_studentController, _subjectController, SelectedSubjectBefore, SelectedSubjectNow);
                showStudentsWhoEnrolled.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please choose a secondary subject to make a condition", "Student condition subject", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void ShowStudents2_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSubjectNow != null)
            {
                ShowStudentsWhoPassedOne showStudentsWhoPassedOne = new ShowStudentsWhoPassedOne(_studentController, _subjectController, SelectedSubjectBefore, SelectedSubjectNow);
                showStudentsWhoPassedOne.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please choose a secondary subject to make a condition", "Student condition subject", MessageBoxButton.OK, MessageBoxImage.Error);
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
