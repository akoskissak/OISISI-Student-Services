using CLI.Controller;
using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for UpdateProfessor.xaml
    /// </summary>
    public partial class UpdateProfessor : Window
    {
        public ProfessorDTO ProfessorDto { get; set; }

        public ObservableCollection<SubjectDTO> SubjectDtos { get; set; }
        public SubjectDTO SelectedSubjectDto { get; set; }

        private ProfessorController _professorController;
        private ProfessorSubjectController _professorSubjectController;

        public event PropertyChangedEventHandler? PropertyChanged;

        public UpdateProfessor(ProfessorController professorController, ProfessorDTO professorDto, double left, double top, double width, double height)
        {
            InitializeComponent();
            DataContext = this;
            ProfessorDto = professorDto;
            this._professorController = professorController;
            this._professorSubjectController = new ProfessorSubjectController();

            SubjectDtos = new ObservableCollection<SubjectDTO>();

            SetInitialWindowSize(left, top, width, height);

            textBoxLastname.TextChanged += TextBox_TextChanged;
            textBoxName.TextChanged += TextBox_TextChanged;
            textBoxStreet.TextChanged += TextBox_TextChanged;
            textBoxNumber.TextChanged += TextBox_TextChanged;
            textBoxCity.TextChanged += TextBox_TextChanged;
            textBoxCountry.TextChanged += TextBox_TextChanged;
            textBoxPhoneNumber.TextChanged += TextBox_TextChanged;
            textBoxEmail.TextChanged += TextBox_TextChanged;
            textBoxIdNumber.TextChanged += TextBox_TextChanged;
            textBoxTitle.TextChanged += TextBox_TextChanged;
            textBoxYearsOfService.TextChanged += TextBox_TextChanged;

            show();
        }
        public void show()
        {
            SubjectDtos.Clear();
            List<Subject> subjects = _professorSubjectController.GetAllSubjectsByProfessorId(ProfessorDto.Id);
            if(subjects.Count > 0)
            {
                foreach(Subject subject in subjects )
                {
                    SubjectDtos.Add(new SubjectDTO(subject));
                }
            }
        }

        private void ValidateTextBoxes()
        {
            updateButton.IsEnabled = ProfessorDto.isValid;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBoxes();
        }

        private void SetInitialWindowSize(double left, double top, double width, double height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ProfessorDto.isValid)
            {
                Professor professor = ProfessorDto.ToProfessor();
                professor.Id = ProfessorDto.Id;
                _professorController.UpdateProfessor(professor);
                Close();
            }
            else
            {
                MessageBox.Show("Professor cannot be updated. Not all fields are valid.");
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _professorController.NotifyObservers();
        }

        private void AddSubject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveSubject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubjectsForProfessorDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
