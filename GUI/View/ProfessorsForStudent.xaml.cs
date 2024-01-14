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
        public ObservableCollection<ProfessorDTO> ProfessorsForStudentDtos { get; set; }

        public ProfessorsForStudent(List<Professor> professors)
        {
            InitializeComponent();
            DataContext = this;

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
    }
}
