using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Model
{
    public class ClassInfoEditDM
    {
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
    }
}
