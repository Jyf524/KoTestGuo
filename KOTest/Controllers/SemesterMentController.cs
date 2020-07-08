using KOTest.Service.Interface;
using KOTest.Service.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KOTest.Controllers
{
    public class SemesterMentController : Controller
    {
        //
        // GET: /SemesterMent/

        //学期列表
        [HttpGet]
        public ActionResult SemesterInfoMentList(String SemesterName, String SemesterState, int? page = 1)
        {
            ISemesterInfoMent SemesterInfoMethod = new SemesterInfoMent();
            return Json(SemesterInfoMethod.SemesterInfoMentList(SemesterName, SemesterState, page), JsonRequestBehavior.AllowGet);

        }

        //删除学期
        [HttpPost]
        public ActionResult SemesterInfoMentDelete(String SemesterID)
        {
            ISemesterInfoMent SemesterInfoMethod = new SemesterInfoMent();
            return Content(SemesterInfoMethod.SemesterInfoMentDelete(SemesterID));
        }

        //学期修改的Get方法
        [HttpGet]
        public ActionResult SemesterInfoEditGet(String SemesterID)
        {
            ISemesterInfoMent SemesterInfoMethod = new SemesterInfoMent();
            if (SemesterInfoMethod.SemesterInfoEditGet(SemesterID) == null)
            {
                return Content(null);
            }
            return Json(SemesterInfoMethod.SemesterInfoEditGet(SemesterID), JsonRequestBehavior.AllowGet);
        }

        //学期修改的Post方法
        [HttpPost]
        public ActionResult SemesterInfoEditPost(String SemesterID, String SemesterName)
        {
            ISemesterInfoMent SemesterInfoMethod = new SemesterInfoMent();
            return Content(SemesterInfoMethod.SemesterInfoEditPost(SemesterID, SemesterName));
        }

        //添加学期
        [HttpPost]
        public ActionResult SemesterInfoAdd(String SemesterName, String SemesterState)
        {
            ISemesterInfoMent SemesterInfoMethod = new SemesterInfoMent();
            return Content(SemesterInfoMethod.SemesterInfoAdd(SemesterName, SemesterState));
        }
    }
}
