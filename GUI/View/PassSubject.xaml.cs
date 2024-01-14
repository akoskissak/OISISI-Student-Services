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
    /// Interaction logic for PassSubject.xaml
    /// </summary>
    public partial class PassSubject : Window
    {
        public SubjectDTO SubjectDto { get; set; }
        private StudentDTO StudentDto { get; set; }
        public ExamGradeDTO ExamGradeDto {  get; set; }
        private StudentController _studentController;
        private ExamGradeController _examGradeController;
        private StudentSubjectController _studentSubjectController;
        private ObservableCollection<ExamGradeDTO> ExamGradeDtos { get; set; }
        public PassSubject(ObservableCollection<ExamGradeDTO> examGradeDtos, StudentController studentController, ExamGradeController examGradeController, StudentDTO studentDto,
            StudentSubjectController studentSubjectController, SubjectDTO subjectDto, double left, double top, double width, double height)
        {
            InitializeComponent();
            DataContext = this;

            SubjectDto = subjectDto;
            StudentDto = studentDto;
            ExamGradeDto = new ExamGradeDTO();

            _studentController = studentController;
            _examGradeController = examGradeController;
            _studentSubjectController = studentSubjectController;
            ExamGradeDtos = examGradeDtos;

            SetInitialWindowSize(left, top, width, height);
            ValidateTextBoxes();
        }

        private void ValidateTextBoxes()
        {
            //ConfirmButton.IsEnabled = ExamGradeDto.Date.ToString() != "" && ExamGradeDto.Grade != -1;
            ConfirmButton.IsEnabled = gradeComboBox.SelectedItem != null && DatePicker.SelectedDate != null;
        }

        private void SetInitialWindowSize(double left, double top, double width, double height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDateTime = DatePicker.SelectedDate;

            if (selectedDateTime.HasValue)
            {
                DateOnly selectedDate = DateOnly.FromDateTime(selectedDateTime.Value);

                ExamGrade examGrade = new ExamGrade(StudentDto.Id, SubjectDto.Id, ExamGradeDto.Grade, selectedDate);

                //ExamGradeDTO examGradeDto = new ExamGradeDTO(examGrade, SubjectDto.ToSubject());
                //_studentController.SetStudentGrade(examGrade, StudentDto.Id);
                _examGradeController.SetStudentGrade(examGrade, StudentDto.Id);
                ExamGradeDtos.Add(ExamGradeDto);
                _studentSubjectController.RemoveStudentSubject(StudentDto.Id, SubjectDto.Id);
                string messageBoxText = "Student passed exam successfully!";
                string caption = "Pass exam";
                MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK);
            }
            Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gradeComboBox.SelectedItem != null)
            {
                ExamGradeDto.Grade = Convert.ToInt32((gradeComboBox.SelectedItem as ComboBoxItem)?.Content);
                ValidateTextBoxes();
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateTextBoxes();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _studentController.NotifyObservers();
            _examGradeController.NotifyObservers();
            _studentSubjectController.NotifyObservers();
        }
    }
}
