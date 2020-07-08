using KOTest.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Interface
{
    public interface IScoreInfoMent
    {
        ScoreListDM ScoreList(String UsersName, String CourseName, int? page = 1);//成绩管理的列表
        String ScoreTemporarySave(Array ScoreID, Array DailyScore, Array InterimScore, Array EndScore, Array AbsenceClass);//成绩管理列表的暂存
        String ScoreRealSave(Array ScoreID, Array DailyScore, Array InterimScore, Array EndScore, Array AbsenceClass);//成绩管理列表的提交
    }
}
