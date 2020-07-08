using KOTest.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Model
{
    public class CourseTeacherListDM
    {
        public int CountPage;
        public int NowPage;
        public int AllPage;
        public IEnumerable<Teacher> CourseTeacherList
        {
            get;
            set;
        }
    }
}
