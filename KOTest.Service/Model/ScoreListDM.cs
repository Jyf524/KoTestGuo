using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Model
{
    public class ScoreListDM
    {
        public int CountPage;
        public int NowPage;
        public int AllPage;
        public IEnumerable<ScoreList> ScoreList
        {
            get;
            set;
        }
    }
    public class ScoreList
    {
        public string ScoreID { get; set; }
        public string UsersID { get; set; }
        public string CourseID { get; set; }
        public string DailyScore { get; set; }
        public string InterimScore { get; set; }
        public string EndScore { get; set; }
        public string AbsenceClass { get; set; }
        public string AllComments { get; set; }
        public string ScoreState { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }

        public string UsersName { get; set; }
        public string IdentityNumber { get; set; }

        public string CourseName { get; set; }
    }
}
