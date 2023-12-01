using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CLI.Model;

namespace GUI.DTO
{
    internal class ProfessorDTO : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProfessorDTO() { }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
