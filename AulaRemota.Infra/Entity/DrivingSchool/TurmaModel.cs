using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Infra.Entity.DrivingSchool
{
    public class TurmaModel
    {
        public TurmaModel()
        {
            Students = new List<StudentModel>();
            Courses = new List<CourseModel>();
        }
        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int DrivingSchoolId { get; set; }
        public DrivingSchoolModel DrivingSchool { get; set; }

        public int InstructorId { get; set; }
        public InstructorModel Instructor { get; set; }

        public virtual ICollection<StudentModel> Students { get; set; }
        public virtual ICollection<CourseModel> Courses { get; set; }
    }
}
