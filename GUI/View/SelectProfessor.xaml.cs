using CLI.Controller;
using CLI.Model;
using CLI.Observer;
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
    /// Interaction logic for SelectProfessor.xaml
    /// </summary>
    public partial class SelectProfessor : Window, IObserver
    {
        private ProfessorController _professorController;
        private ProfessorSubjectController _professorSubjectController;
        public ObservableCollection<ProfessorDTO> ProfessorDtos { get; set; }
        private ProfessorDTO? SelectedProfessor { get; set; }
        private ProfessorDTO? ProfessorForSubjectDto { get; set; }
        private SubjectDTO SubjectDto { get; set; }
        public SelectProfessor(SubjectDTO subjectDTO, ProfessorDTO professorForSubjectDto, ProfessorSubjectController professorSubjectController, double left, double top, double width, double height)
        {
            InitializeComponent();
            DataContext = this;
            this._professorController = new ProfessorController();
            this.ProfessorDtos = new ObservableCollection<ProfessorDTO>();
            this.SubjectDto = subjectDTO;
            this._professorSubjectController = professorSubjectController;
            this.ProfessorForSubjectDto = professorForSubjectDto;

            SetInitialWindowSize(left, top, width, height);

            Update();
        }

        private void SetInitialWindowSize(double left, double top, double width, double height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        public void Update()
        {
            ProfessorDtos.Clear();
            foreach (Professor professor in _professorController.GetAllProfessors())
            {
                ProfessorDtos.Add(new ProfessorDTO(professor));
            }
        }

        private void SelectProfessorDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedProfessor = SelectProfessorsDataGrid.SelectedItem as ProfessorDTO;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProfessor == null)
            {
                string messageBoxText = "Please select a professor!";
                string caption = "Select professor";
                MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                SubjectDto.Professor = SelectedProfessor.ToProfessor();
                SubjectDto.Professor.Id = SelectedProfessor.Id;
                SubjectDto.ProfessorId = SelectedProfessor.Id;
                ProfessorForSubjectDto = SelectedProfessor;
                string messageBoxText = "Professor assigned successfully!";
                string caption = "Professor assigned";
                MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK);
                Close();
            }
        }
    }
}
