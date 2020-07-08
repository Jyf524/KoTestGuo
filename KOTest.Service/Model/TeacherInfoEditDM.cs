using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Model
{
    public class TeacherInfoEditDM
    {
        public string TeacherID { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSex { get; set; }
        public string IdentityNumber { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
    }
}
