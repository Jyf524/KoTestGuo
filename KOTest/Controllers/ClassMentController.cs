using KOTest.Service.Interface;
using KOTest.Service.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KOTest.Controllers
{
    public class ClassMentController : Controller
    {
        //
        // GET: /ClassMent/

        //班级列表
        [HttpGet]
        public ActionResult ClassInfoMentList(String ClassName, String DepartmentName, int? page = 1)
        {
            IClassInfoMent ClassInfoMethod = new ClassInfoMent();
            return Json(ClassInfoMethod.ClassInfoMentList(ClassName, DepartmentName, page), JsonRequestBehavior.AllowGet);

        }

        //删除班级
        [HttpPost]
        public ActionResult ClassInfoMentDelete(String ClassID)
        {
            IClassInfoMent ClassInfoMethod = new ClassInfoMent();
            return Content(ClassInfoMethod.ClassInfoMentDelete(ClassID));
        }

        //班级修改的Get方法
        [HttpGet]
        public ActionResult ClassInfoEditGet(String ClassID)
        {
            IClassInfoMent ClassInfoMethod = new ClassInfoMent();
            if (ClassInfoMethod.ClassInfoEditGet(ClassID) == null)
            {
                return Content(null);
            }
            return Json(ClassInfoMethod.ClassInfoEditGet(ClassID), JsonRequestBehavior.AllowGet);
        }

        //班级修改的Post方法
        [HttpPost]
        public ActionResult ClassInfoEditPost(String ClassID, String ClassName)
        {
            IClassInfoMent ClassInfoMethod = new ClassInfoMent();
            return Content(ClassInfoMethod.ClassInfoEditPost(ClassID, ClassName));
        }

        //添加班级
        [HttpPost]
        public ActionResult ClassInfoAdd(String ClassName, String DepartmentName, String MajorName)
        {
            IClassInfoMent ClassInfoMethod = new ClassInfoMent();
            return Content(ClassInfoMethod.ClassInfoAdd(ClassName, DepartmentName, MajorName));
        }

    }
}
