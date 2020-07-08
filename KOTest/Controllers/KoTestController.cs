using KOTest.Models;
using KOTest.Service.Interface;
using KOTest.Service.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeMent.Util;

namespace KOTest.Controllers
{
    public class KoTestController : Controller
    {
        //
        // GET: /KoTest/

        //登录
        public ActionResult UserLogin(String UserName, String Password, String LoginCode)
        {

            IBackmentService FoveMethod = new BackmentService();
            string ConfirmCode = Session["CheckCode"].ToString().ToLower();

            return Content(FoveMethod.Login(UserName, Password, LoginCode, ConfirmCode));

        }

        public ActionResult Login()//登录
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClassMentIndex()
        {
            return View();
        }

        public ActionResult SemesterMentIndex()
        {
            return View();
        }

        public ActionResult CourseMentIndex()
        {
            return View();
        }

        public ActionResult CourseMentAdd()
        {
            return View();
        }

        public ActionResult TeacherMentIndex()
        {
            return View();
        }
         public ActionResult ChoiceCourseMentIndex()
        {
            return View();
        }
         public ActionResult ScoreMentIndex()
         {
             return View();
         }
    }
}
