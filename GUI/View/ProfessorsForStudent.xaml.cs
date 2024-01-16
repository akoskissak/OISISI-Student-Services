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
    /// Interaction logic for ProfessorsForStudent.xaml
    /// </summary>
    public partial class ProfessorsForStudent : Window
    {
        private App app;
        private const string SRB = "sr-RS";
        private const string ENG = "en-US";
        public ObservableCollection<ProfessorDTO> ProfessorsForStudentDtos { get; set; }

        public ProfessorsForStudent(List<Professor> professors)
        {
            InitializeComponent();
            DataContext = this;
            app = (App)Application.Current;

            ProfessorsForStudentDtos = new ObservableCollection<ProfessorDTO>();
            foreach (Professor p in professors)
                ProfessorsForStudentDtos.Add(new ProfessorDTO(p));

            Update(professors);
        }

        private void Update(List<Professor> professors)
        {
            ProfessorsForStudentDtos.Clear();
            foreach (Professor professor in professors)
                ProfessorsForStudentDtos.Add(new ProfessorDTO(professor));
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
