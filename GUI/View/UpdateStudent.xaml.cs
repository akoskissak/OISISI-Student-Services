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
    public partial class UpdateStudent : Window, IObserver, INotifyPropertyChanged
    {
        private App app;
        private const string SRB = "sr-RS";
        private const string ENG = "en-US";
        public StudentDTO StudentDto { get; set; }
        public ExamGradeDTO ExamGradeDto { get; set; }

        private StudentController _studentController;
        private StudentSubjectController _studentSubjectController;
        private SubjectController _subjectController;

        private ExamGradeController _examGradeController;

        public ObservableCollection<SubjectDTO> SubjectDtos { get; set; }
        public ObservableCollection<ExamGradeDTO> ExamGradeDtos { get; set; }

        public SubjectDTO SelectedSubjectDto { get; set; }
        public ExamGradeDTO SelectedExam { get; set; }

        public UpdateStudent(StudentController _studentController, ExamGradeController examGradeController, StudentDTO studentDto, SubjectController subjectController , double left, double top, double width, double height)
        {
            InitializeComponent();
            DataContext = this;
            app = (App)Application.Current;

            StudentDto = studentDto;
            this._studentController = _studentController;
            this._examGradeController = examGradeController;
            this._subjectController = subjectController;
            this._examGradeController.Subscribe(this);

            ExamGradeDtos = new ObservableCollection<ExamGradeDTO>();

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
            _studentSubjectController.NotifyObservers();
            _examGradeController.NotifyObservers();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if(SubmittedDataGrid.SelectedItem != null)
            {
                if(SelectedExam == null)
                {
                    MessageBox.Show("Plase choose a grade to remove!");
                }
                else
                {
                    string messageBoxText = "Are you sure you want to remove grade?";
                    string caption = "Remove grade";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, MessageBoxImage.Question);
                    if(result == MessageBoxResult.Yes)
                    {
                        int subjectId = SelectedExam.SubjectId;
                        if (_examGradeController.RemoveGradeForStudent(StudentDto.Id, SelectedExam.SubjectId))
                        {
                            Subject s = _subjectController.GetSubjectById(subjectId);
                            TotalESPB -= s.Espb;
                            _studentSubjectController.AddStudentSubject(new StudentSubject(StudentDto.Id, subjectId));


                            ExamGradeDtos.Remove(SelectedExam);
                            Update();
                            MessageBox.Show("Grade removed successfully.", "Remove Successful", MessageBoxButton.OK);
                        }
                        else
                            MessageBox.Show("Grade not removed.", "ERROR", MessageBoxButton.OK);
                    }
                }
            }
        }

        private void Add_Subject_Click(object sender, RoutedEventArgs e)
        {
            AddSubjectForStudent addSubjectForStudent = new AddSubjectForStudent(_studentController, StudentDto, SubjectDtos);
            addSubjectForStudent.ShowDialog();
        }

        private void Clear_Subject_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSubjectDto != null)
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
            else
            {
                string messageBoxText = "Please choose a subject to remove!";
                string caption = "Remove subject";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBox.Show(messageBoxText, caption, button, MessageBoxImage.Error);
            }
        }

        private void Pass_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSubjectDto != null)
            {
                PassSubject passSubject = new PassSubject(ExamGradeDtos, _studentController, _examGradeController, StudentDto, _studentSubjectController, SelectedSubjectDto, _subjectController, TotalESPB, Left, Top, Width, Height);
                passSubject.ShowDialog();
                Update();
            }
            else
            {
                string messageBoxText = "Please choose a subject to pass!";
                string caption = "Pass subject";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBox.Show(messageBoxText, caption, button, MessageBoxImage.Error);
            }
        }

        private void UnsubmittedDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedSubjectDto = UnsubmittedDataGrid.SelectedItem as SubjectDTO;
        }

        private void SubmittedDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedExam = SubmittedDataGrid.SelectedItem as ExamGradeDTO;
        }

        public void Update()
        {
            SubjectDtos.Clear();
            foreach (Subject subject in _studentController.GetStudentById(StudentDto.Id).UnsubmittedSubjects)
            {
                SubjectDtos.Add(new SubjectDTO(subject));
            }

            ExamGradeDtos.Clear();
            TotalESPB = 0;
            foreach (ExamGrade examGrade in _studentController.GetStudentById(StudentDto.Id).Grades)
            {
                TotalESPB += examGrade.Subject.Espb;
                ExamGradeDtos.Add(new ExamGradeDTO(examGrade, examGrade.Subject));
            }
            StudentDto.AverageGrade = _studentController.GetStudentById(StudentDto.Id).AverageGrade;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _totalESPB;
        public int TotalESPB
        {
            get { return _totalESPB; }
            set
            {
                if (_totalESPB != value)
                {
                    _totalESPB = value;
                    OnPropertyChanged(nameof(TotalESPB));
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) && Keyboard.IsKeyDown(Key.E))
                MenuItem_Click_English(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftShift) && Keyboard.IsKeyDown(Key.R))
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
