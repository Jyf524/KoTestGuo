using KOTest.Service.Interface;
using KOTest.Service.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KOTest.Controllers
{
    public class ChoiceCourseController : Controller
    {
        //
        // GET: /ChoiceCourse/

        //选课管理的列表
        [HttpGet]
        public ActionResult ChoiceCourseList(String CourseName, String ClassName, int? page = 1)
        {
            IChoiceCourseMent ChoiceCourseMethod = new ChoiceCourseMent();
            return Json(ChoiceCourseMethod.ChoiceCourseList(CourseName, ClassName, page), JsonRequestBehavior.AllowGet);
        }

        //班级导航条
        [HttpGet]
        public ActionResult ClassNavList()
        {
            IChoiceCourseMent ChoiceCourseMethod = new ChoiceCourseMent();
            return Json(ChoiceCourseMethod.ClassNavList(), JsonRequestBehavior.AllowGet);
        }

        //添加选课
        [HttpPost]
        public ActionResult ChoiceCourseAdd(String ClassName, String CourseName, String WeekClassHours, String IsnotExamination, String CourseKind)
        {
            IChoiceCourseMent ChoiceCourseMethod = new ChoiceCourseMent();
            return Content(ChoiceCourseMethod.ChoiceCourseAdd(ClassName,CourseName,WeekClassHours,IsnotExamination,CourseKind));
        }

        //分配老师列表
        [HttpGet]
        public ActionResult CourseTeacherList()
        {
            IChoiceCourseMent ChoiceCourseMethod = new ChoiceCourseMent();
            return Json(ChoiceCourseMethod.CourseTeacherList(), JsonRequestBehavior.AllowGet);
        }

        //分配老师
        [HttpPost]
        public ActionResult CourseTeacherAdopt(String ChoiceCourseID,Array CourseTeacherID)
        {
            var str = CourseTeacherID.GetValue(0).ToString();
            var getArr = str.Split(',');

            IChoiceCourseMent ChoiceCourseMethod = new ChoiceCourseMent();
            return Json(ChoiceCourseMethod.CourseTeacherAdopt(ChoiceCourseID, getArr), JsonRequestBehavior.AllowGet);
        }
    }
}