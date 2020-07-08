using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Repository.Entities
{
    [Table("Teacher")]
    public class Teacher
    {
        [Key]
        public string TeacherID { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSex { get; set; }
        public string IdentityNumber { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }

    }
}
