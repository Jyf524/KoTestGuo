using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Model
{
    public class CourseInfoEditDM
    {
        public string CourseID { get; set; }
        public string CourseNumber { get; set; }
        public string CourseName { get; set; }
        public string CourseImg { get; set; }
        public string FullName { get; set; }
        public string FirstSpelling { get; set; }
        public string AllSpelling { get; set; }
        public string CourseKind { get; set; }
        public string DepartmentName { get; set; }
        public string Introduction { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
    }
}
