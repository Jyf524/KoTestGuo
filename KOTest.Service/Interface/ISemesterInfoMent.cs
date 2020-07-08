using KOTest.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Interface
{
    public interface ISemesterInfoMent
    {
        SemesterInfoMentListDM SemesterInfoMentList(String SemesterName, String SemesterState, int? page = 1);//学期列表
        String SemesterInfoMentDelete(String SemesterID);//删除学期
        SemesterInfoEditDM SemesterInfoEditGet(String SemesterID);//学期修改的Get方法
        String SemesterInfoEditPost(String SemesterID, String SemesterName);//学期修改的Post方法
        String SemesterInfoAdd(String SemesterName, String SemesterState);//添加学期
    }
}
