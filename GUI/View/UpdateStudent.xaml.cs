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
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for UpdateStudent.xaml
    /// </summary>
    public partial class UpdateStudent : Window
    {
        
        public StudentDTO StudentDto { get; set; }
        private StudentController _studentController;

        public UpdateStudent(StudentController _studentController, StudentDTO studentDto)
        {
            InitializeComponent();
            DataContext = this;
            StudentDto = studentDto;
            this._studentController = _studentController;

            statusComboBox.Items.Clear();
            statusComboBox.ItemsSource = Enum.GetValues(typeof(Status));

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
    }
}
