using KOTest.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Interface
{
    public interface IClassInfoMent
    {
        ClassInfoMentListDM ClassInfoMentList(String ClassName, String DepartmentName, int? page = 1);//班级列表
        String ClassInfoMentDelete(String ClassID);//删除班级
        ClassInfoEditDM ClassInfoEditGet(String ClassID);//班级修改的Get方法
        String ClassInfoEditPost(String ClassID, String ClassName);//班级修改的Post方法
        String ClassInfoAdd(String ClassName, String DepartmentName, String MajorName);//添加班级
    }
}
