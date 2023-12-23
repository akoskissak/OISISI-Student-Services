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
    /// Interaction logic for UpdateSubject.xaml
    /// </summary>
    public partial class UpdateSubject : Window
    {
        public SubjectDTO SubjectDto { get; set; }
        private SubjectController _subjectController;

        public event PropertyChangedEventHandler? PropertyChanged;

        public UpdateSubject(SubjectController subjectController, SubjectDTO subjectDto)
        {
            InitializeComponent();
            DataContext = this;
            SubjectDto = subjectDto;
            this._subjectController = subjectController;

        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            Subject subject = SubjectDto.ToSubject();
            subject.Id = SubjectDto.Id;
            _subjectController.UpdateSubject(subject);
            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
