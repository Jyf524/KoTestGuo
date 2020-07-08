using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Model
{
    public class ChoiceCourseListDM
    {
        public int CountPage;
        public int NowPage;
        public int AllPage;
        public IEnumerable<ChoiceCourseList> ChoiceCourseList
        {
            get;
            set;
        }
    }
    public class ChoiceCourseList
    {
        public string ChoiceCourseID { get; set; }
        public string ClassName { get; set; }
        public string CourseName { get; set; }
        public string WeekClassHours { get; set; }
        public string IsnotExamination { get; set; }
        public string CourseKind { get; set; }
        public string CourseTeacherID { get; set; }
        public string CourseTeacherName { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
    }
}
