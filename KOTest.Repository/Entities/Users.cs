using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Repository.Entities
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public string UsersID { get; set; }
        public string UsersWorkID { get; set; }
        public string UsersName { get; set; }
        public string Password { get; set; }
        public string IdentityNumber { get; set; }
        public string UsersSex { get; set; }
        public string UsersAge { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }

    }
}
