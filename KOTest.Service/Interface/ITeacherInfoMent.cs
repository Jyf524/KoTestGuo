using KOTest.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Interface
{
    public interface ITeacherInfoMent
    {
        TeacherInfoMentListDM TeacherInfoMentList(String TeacherName, String TeacherSex, int? page = 1);//教师列表
        String TeacherInfoMentDelete(String TeacherID);//删除教师
        TeacherInfoEditDM TeacherInfoEditGet(String TeacherID);//教师修改的Get方法
        String TeacherInfoEditPost(String TeacherID, String TeacherName, String TeacherSex, String IdentityNumber);//教师修改的Post方法
        String TeacherInfoAdd(String TeacherName, String TeacherSex, String IdentityNumber);//添加教师
    }
}
