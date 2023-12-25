using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class ProfessorSubjectDTO
    {
        public int ProfessorId { get ; set; }
        public int SubjectId { get; set; }
        public int Id { get; set; }

        public ProfessorSubjectDTO() {}
        public ProfessorSubjectDTO(int professorId, int subjectId)
        {
            ProfessorId = professorId;
            SubjectId = subjectId;
        }
    }
}
