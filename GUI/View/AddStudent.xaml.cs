using CLI.DAO;
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
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window, INotifyPropertyChanged
    {
        public StudentDTO StudentDto { get; set; }

        private StudentDAO _studentDao;
        public event PropertyChangedEventHandler? PropertyChanged;
        public AddStudent(StudentDAO studentDao)
        {
            InitializeComponent();
            DataContext = this;
            StudentDto = new StudentDTO();
            this._studentDao = studentDao;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            _studentDao.AddStudent(StudentDto.ToStudent());
            Close();
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            StudentDto.Lastname = "";
            StudentDto.Name = "";
            StudentDto.DateOfBirth = "11/11/2021";
            StudentDto.Street = "";
            StudentDto.Number = "";
            StudentDto.City = "";
            StudentDto.Country = "";
            StudentDto.PhoneNumber = "";
            StudentDto.Email = "";
            StudentDto.Status = 0;
            StudentDto.CurrentYearOfStudy = 0;
            StudentDto.EnrollmentNumber = 0;
            StudentDto.EnrollmentYear = 0;
            StudentDto.AverageGrade = 0;
            Close();
        }
    }
}
