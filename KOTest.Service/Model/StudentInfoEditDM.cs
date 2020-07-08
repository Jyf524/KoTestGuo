using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Model
{
    public class StudentInfoEditDM
    {
        public string UsersID { get; set; }
        public string UsersWorkID { get; set; }
        public string UsersName { get; set; }
        public string Password { get; set; }
        public string UsersSex { get; set; }
        public string UsersAge { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
    }
}
