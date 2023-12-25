using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class StudentSubjectDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        public StudentSubjectDTO() {}
        public StudentSubjectDTO(int studentId, int subjectId)
        {
            StudentId = studentId;
            SubjectId = subjectId;
        }
    }
}
