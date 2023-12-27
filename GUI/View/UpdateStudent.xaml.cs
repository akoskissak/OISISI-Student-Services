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
    public partial class UpdateStudent : Window
    {
        public StudentDTO StudentDto { get; set; }
        public ExamGradeDTO ExamGradeDto { get; set; }

        private StudentController _studentController;

        private ExamGradeController _examGradeController;

        public UpdateStudent(StudentController _studentController, StudentDTO studentDto, double left, double top, double width, double height)
        {
            InitializeComponent();
            DataContext = this;
            StudentDto = studentDto;
            this._studentController = _studentController;

            statusComboBox.Items.Clear();
            statusComboBox.ItemsSource = Enum.GetValues(typeof(Status));

            SetInitialWindowSize(left, top, width, height);
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

        }

        private void Pass_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
