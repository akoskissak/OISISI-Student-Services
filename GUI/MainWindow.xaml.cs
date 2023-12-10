using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using GUI.DTO;
using GUI.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        public ObservableCollection<ProfessorDTO> ProfessorDtos { get; set; }
        public ProfessorDTO SelectedProfessor { get; set; }
        private ProfessorDAO _professorDao { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ProfessorDtos = new ObservableCollection<ProfessorDTO>();
            _professorDao = new ProfessorDAO();
            _professorDao.ProfessorObservable.Subscribe(this);
            Update();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void MouseEnterAdd(object sender, MouseEventArgs e)
        {

        }
        private void ClickAddProfessor(object sender, RoutedEventArgs e)
        {
            AddProfessor addProfessorWindow = new AddProfessor(_professorDao);
            addProfessorWindow.Show();
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        public void Update()
        {
            ProfessorDtos.Clear();
            foreach (Professor professor in _professorDao.GetAllProfessors())
                ProfessorDtos.Add(new ProfessorDTO(professor));
        }
    }
}
