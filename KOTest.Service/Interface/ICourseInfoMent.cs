using KOTest.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Interface
{
    public interface ICourseInfoMent
    {
        //课程列表
        CourseInfoMentListDM CourseInfoMentList(String SearchIngTxt, String CourseKind, String DepartmentName, int? page = 1);
        //删除课程
        String CourseInfoMentDelete(String CourseID);
        //课程修改的Get方法
        CourseInfoEditDM CourseInfoEditGet(String CourseID);
        //课程修改的Post方法
        String CourseInfoEditPost(String CourseID, String CourseNumber, String CourseName, String FullName, String FirstSpelling, String AllSpelling, String CourseKind, String DepartmentName, String Introduction);
        //添加课程
        String CourseInfoAdd(String CourseImg, String CourseNumber, String CourseName, String FullName, String FirstSpelling, String AllSpelling, String CourseKind, String DepartmentName, String Introduction);
    }
}
