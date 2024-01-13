using CLI.Controller;
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
    /// Interaction logic for DepartmentView.xaml
    /// </summary>
    public partial class DepartmentView : Window
    {
        private DepartmentController _departmentController {  get; set; }
        public ObservableCollection<DepartmentDTO> DepartmentDtos { get; set; }

        private DepartmentDTO SelectedDepartment { get; set; }

        public DepartmentView(double left, double top, double width, double height)
        {
            InitializeComponent();
            DataContext = this;
            DepartmentDtos = new ObservableCollection<DepartmentDTO>();
            _departmentController = new DepartmentController();
            SetInitialWindowSize(left, top, width, height);

            Update();
        }

        private void SetInitialWindowSize(double left, double top, double width, double height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }
        public void Update()
        {
            DepartmentDtos.Clear();
            foreach (CLI.Model.Department department in _departmentController.getAllDepartments())
                DepartmentDtos.Add(new DepartmentDTO(department));
        }

        private void ShowProfessors_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedDepartment != null)
            {
                ProfessorsForDepartment professorsForDepartment = new ProfessorsForDepartment(SelectedDepartment, _departmentController);
                professorsForDepartment.ShowDialog();
                Update();
                _departmentController.NotifyObservers();
            }
            else
            {
                string messageBoxText = "Please select a department!";
                string caption = "Select department";
                MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void DepartmentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDepartment = DepartmentDataGrid.SelectedItem as DepartmentDTO;
        }

        private void ShowSubjects_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDepartment != null)
            {
                SubjectsForDepartment subjectsForDepartment = new SubjectsForDepartment(SelectedDepartment, _departmentController);
                subjectsForDepartment.ShowDialog();
            }
            else
            {
                string messageBoxText = "Please select a department!";
                string caption = "Select department";
                MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
