using CLI.Controller;
using CLI.Model;
using GUI.DTO;
using System;
using System.Windows;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        public StudentDTO StudentDto { get; set; }

        private StudentController _studentController;
        public AddStudent(StudentController studentController, double left,  double top,  double width, double height)
        {
            InitializeComponent();
            DataContext = this;
            StudentDto = new StudentDTO();
            this._studentController = studentController;

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

            ValidateTextBoxes();
        }

        private void ValidateTextBoxes()
        {
            addButton.IsEnabled = StudentDto.IsValid;
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
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (StudentDto.IsValid)
            {
                _studentController.Add(StudentDto.ToStudent());
                Close();
            }
            else
            {
                MessageBox.Show("Student can not be created. Not all fields are valid.");
            }
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
