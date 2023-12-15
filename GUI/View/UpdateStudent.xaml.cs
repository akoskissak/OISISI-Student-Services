using CLI.Controller;
using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
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

        private StudentController studentController;

        public UpdateStudent(StudentController studentController, StudentDTO studentDto)
        {
            InitializeComponent();
            DataContext = this;
            StudentDto = studentDto;
            this.studentController = studentController;

            statusComboBox.Items.Clear();
            statusComboBox.ItemsSource = Enum.GetValues(typeof(Status));
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            Student student = StudentDto.ToStudent();
            student.Id = StudentDto.Id;
            studentController.Update(student);
            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
