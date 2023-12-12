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
    /// Interaction logic for AddSubject.xaml
    /// </summary>
    public partial class AddSubject : Window, INotifyPropertyChanged
    {
        public SubjectDTO SubjectDto { get; set; }

        private SubjectDAO _subjectDao;

        public event PropertyChangedEventHandler? PropertyChanged;
        public AddSubject(SubjectDAO subjectDao)
        {
            InitializeComponent();
            DataContext = this;
            SubjectDto = new SubjectDTO();
            this._subjectDao = subjectDao;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            _subjectDao.AddSubject(SubjectDto.ToSubject());
            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
