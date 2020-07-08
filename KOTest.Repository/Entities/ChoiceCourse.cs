using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Repository.Entities
{
    [Table("ChoiceCourse")]
    public class ChoiceCourse
    {
        [Key]
        public string ChoiceCourseID { get; set; }
        public string ClassName { get; set; }
        public string CourseName { get; set; }
        public string WeekClassHours { get; set; }
        public string IsnotExamination { get; set; }
        public string CourseKind { get; set; }
        public string CourseTeacherID { get; set; }
        public string CourseTeacherName { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }

    }
}
