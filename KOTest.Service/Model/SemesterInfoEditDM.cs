using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Model
{
    public class SemesterInfoEditDM
    {
        public string SemesterID { get; set; }
        public string SemesterName { get; set; }
        public string SemesterState { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
    }
}
