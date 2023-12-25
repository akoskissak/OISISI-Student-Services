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
    }
}
