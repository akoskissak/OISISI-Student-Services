using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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

            statusComboBox.Items.Clear();
            statusComboBox.ItemsSource = Enum.GetValues(typeof(Status));
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
            Close();
        }

    }
}
