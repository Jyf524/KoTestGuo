using KOTest.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Interface
{
    public interface IStudentInfoMent
    {
        StudentInfoMentListDM StudentInfoMentList(String UsersName, String UsersSex, int? page = 1);//学生列表
        String StudentInfoMentDelete(Array arr);//删除学生
        StudentInfoEditDM StudentInfoEditGet(String UsersID);//学生修改的Get方法
        String StudentInfoEditPost(String UsersID, String UsersName);//学生修改的Post方法
        String StudentInfoAdd(String UsersWorkID, String UsersName,String Password, String UsersSex, String UsersAge);//添加学生
        StudentExportListDM StudentExportList();//导出学生数据
    }
}
