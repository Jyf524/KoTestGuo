using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Repository.Entities
{
    [Table("Class")]
    public class Class
    {
        [Key]
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string MajorName { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }

    }
}
