﻿using CLI.DAO;
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
    /// Interaction logic for UpdateProfessor.xaml
    /// </summary>
    public partial class UpdateProfessor : Window
    {
        public ProfessorDTO ProfessorDto { get; set; }

        private ProfessorDAO _professorDao;

        public event PropertyChangedEventHandler? PropertyChanged;

        public UpdateProfessor(ProfessorDAO professorDao, ProfessorDTO professorDto)
        {
            InitializeComponent();
            DataContext = this;
            ProfessorDto = professorDto;
            this._professorDao = professorDao;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            Professor professor = ProfessorDto.ToProfessor();
            professor.Id = ProfessorDto.Id;
            _professorDao.UpdateProfessor(professor);
            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
