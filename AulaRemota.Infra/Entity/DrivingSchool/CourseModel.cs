using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Infra.Entity.DrivingSchool
{
    public class CourseModel
    {
        public CourseModel()
        {
            this.Classes = new List<TurmaModel>();
        }

        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Code { get; set; }
        public int Workload { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Description { get; set; }
        
        public int InstructorId { get; set; }
        public InstructorModel Instructor { get; set; }

        public int DrivingSchoolId { get; set; }
        public virtual DrivingSchoolModel DrivingSchool { get; set; }

        public virtual ICollection<TurmaModel> Classes { get; set; }
    }
}
