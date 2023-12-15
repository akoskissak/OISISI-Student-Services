using CLI.Controller;
using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        public StudentDTO StudentDto { get; set; }

        private StudentController studentController;
        public AddStudent(StudentController studentController)
        {
            InitializeComponent();
            DataContext = this;
            StudentDto = new StudentDTO();
            this.studentController = studentController;

            statusComboBox.Items.Clear();
            statusComboBox.ItemsSource = Enum.GetValues(typeof(Status));
        }
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            studentController.Add(StudentDto.ToStudent());
            Close();
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
