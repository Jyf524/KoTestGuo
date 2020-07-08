using KOTest.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Interface
{
    public interface IChoiceCourseMent
    {
        ChoiceCourseListDM ChoiceCourseList(String CourseName, String ClassName, int? page = 1);//选课管理的列表
        ClassNavListDM ClassNavList();//课程导航条
        String ChoiceCourseAdd(String ClassName, String CourseName, String WeekClassHours, String IsnotExamination, String CourseKind);//添加选课
        CourseTeacherListDM CourseTeacherList();//分配老师列表
        String CourseTeacherAdopt(String ChoiceCourseID,Array CourseTeacherID);
    }
}
