using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Repository.Entities
{
    [Table("Score")]
    public class Score
    {
        [Key]
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
    }
}
