using CLI.Controller;
using CLI.DAO;
using CLI.Model;
using CLI.Observer;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for UpdateStudent.xaml
    /// </summary>
    public partial class UpdateStudent : Window, IObserver
    {
        public StudentDTO StudentDto { get; set; }
        public ExamGradeDTO ExamGradeDto { get; set; }

        private StudentController _studentController;
        private StudentSubjectController _studentSubjectController;

        private ExamGradeController _examGradeController;
        
        public ObservableCollection<SubjectDTO> SubjectDtos { get; set; }

        public SubjectDTO SelectedSubjectDto { get; set; }

        public UpdateStudent(StudentController _studentController, StudentDTO studentDto, double left, double top, double width, double height)
        {
            InitializeComponent();
            DataContext = this;
            StudentDto = studentDto;
            this._studentController = _studentController;
            this._studentSubjectController = new StudentSubjectController();

            SubjectDtos = new ObservableCollection<SubjectDTO>();

            statusComboBox.Items.Clear();
            statusComboBox.ItemsSource = Enum.GetValues(typeof(Status));

            SetInitialWindowSize(left, top, width, height);

            textBoxLastName.TextChanged += TextBox_TextChanged;
            textBoxName.TextChanged += TextBox_TextChanged;
            textBoxNumber.TextChanged += TextBox_TextChanged;
            textBoxPhoneNumber.TextChanged += TextBox_TextChanged;
            textBoxStreet.TextChanged += TextBox_TextChanged;
            textBoxStudyProgrammeMark.TextChanged += TextBox_TextChanged;
            textBoxEnrollmentYear.TextChanged += TextBox_TextChanged;
            textBoxEnrollmentNumber.TextChanged += TextBox_TextChanged;
            textBoxEmail.TextChanged += TextBox_TextChanged;
            textBoxCurrentYearOfStudy.TextChanged += TextBox_TextChanged;
            textBoxCountry.TextChanged += TextBox_TextChanged;
            textBoxCity.TextChanged += TextBox_TextChanged;


            Update();
        }
        private void ValidateTextBoxes()
        {
            updateButton.IsEnabled = StudentDto.IsValid;
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

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (StudentDto.IsValid)
            {
                Student student = StudentDto.ToStudent();
                student.Id = StudentDto.Id;
                _studentController.Update(student);
                Close();
            }
            else
            {
                MessageBox.Show("Student can not be updated. Not all fields are valid.");
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _studentController.NotifyObservers();
        }

        private void Ponisti_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Subject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Clear_Subject_Click(object sender, RoutedEventArgs e)
        {
            if (UnsubmittedDataGrid.SelectedItem != null)
            {
                if (SelectedSubjectDto == null)
                    MessageBox.Show("Please choose a subject to remove!");
                else
                {
                    string messageBoxText = "Are you sure you want to remove subject?";
                    string caption = "Remove subject";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (_studentSubjectController.RemoveSubjectForStudent(StudentDto.Id, SelectedSubjectDto.Id))
                        {
                            SubjectDtos.Remove(SelectedSubjectDto);
                            MessageBox.Show("Subject removed successfully.", "Remove Successful", MessageBoxButton.OK);
                        }
                        else
                            MessageBox.Show("Subject not removed.", "ERROR", MessageBoxButton.OK);
                    }
                }
            }
        }

        private void Pass_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UnsubmittedDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedSubjectDto = UnsubmittedDataGrid.SelectedItem as SubjectDTO;
        }

        public void Update()
        {
            SubjectDtos.Clear();
            foreach (Subject subject in _studentController.GetStudentById(StudentDto.Id).UnsubmittedSubjects)
            {
                SubjectDtos.Add(new SubjectDTO(subject));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
