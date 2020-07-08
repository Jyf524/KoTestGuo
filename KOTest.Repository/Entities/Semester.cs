using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Repository.Entities
{
    [Table("Semester")]
    public class Semester
    {
        [Key]
        public string SemesterID { get; set; }
        public string SemesterName { get; set; }
        public string SemesterState { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }

    }
}
