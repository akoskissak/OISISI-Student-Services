﻿using CLI.Controller;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for AddSubjectForProfessor.xaml
    /// </summary>
    public partial class AddSubjectForProfessor : Window
    {
        private ProfessorDTO SelectedProfessor { get; set; }
        public ObservableCollection<SubjectDTO> SubjectDtosWithoutProfessor { get; set; }
        public ObservableCollection<SubjectDTO> SubjectDtos { get; set; }

        private ProfessorController _professorController;
        private ProfessorSubjectController _professorSubjectController;
        private SubjectController _subjectController;
        public List<SubjectDTO> SelectedSubjectDto { get; set; }

        public AddSubjectForProfessor(ProfessorController professorController, ProfessorDTO selectedProfessor, ObservableCollection<SubjectDTO> sdtos)
        {
            InitializeComponent();
            DataContext = this;
            this._professorController = professorController;
            this._professorSubjectController = new ProfessorSubjectController();
            this._subjectController = new SubjectController();

            this.SubjectDtos = sdtos;
            SubjectDtosWithoutProfessor = new ObservableCollection<SubjectDTO>();
            SelectedProfessor = selectedProfessor;

            Update();
        }
        
        private void Update()
        {
            SubjectDtosWithoutProfessor.Clear();
            List<Subject> subjects = _professorSubjectController.GetAllSubjectsNotByProfessorId(SelectedProfessor.Id);
            foreach (Subject subject in subjects)
            {
                SubjectDtosWithoutProfessor.Add(new SubjectDTO(subject));
            }
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSubjectDto == null)
            {
                MessageBox.Show("Please choose a subject to add!");
            }
            else
            {
                foreach(SubjectDTO sdto in SelectedSubjectDto)
                {
                    Professor professor = _professorController.GetProfessorById(SelectedProfessor.Id);
                    _professorSubjectController.SetProfessorForSubject(sdto.Id, professor);
                    _professorSubjectController.SetSubjetcProfessor(sdto.Id, professor);
                    SubjectDtos.Add(sdto);
                }
                Close();
            }
        }
        private void SubjectsToAddForProfessorDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedSubjectDto = SubjectsToAddForProfessorDataGrid.SelectedItems.Cast<SubjectDTO>().ToList();
        }

    }
}
