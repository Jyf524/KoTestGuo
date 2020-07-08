using KOTest.Service.Interface;
using KOTest.Service.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KOTest.Controllers
{
    public class ScoreController : Controller
    {
        //
        // GET: /Score/

        //成绩管理的列表
        [HttpGet]
        public ActionResult ScoreList(String UsersName, String CourseName, int? page = 1)
        {
            IScoreInfoMent ScoreMethod = new ScoreInfoMent();
            return Json(ScoreMethod.ScoreList(UsersName, CourseName, page), JsonRequestBehavior.AllowGet);
        }

        //成绩管理列表的暂存
        [HttpPost]
        public ActionResult ScoreTemporarySave(Array arrScoreID, Array arrDailyScore, Array arrInterimScore, Array arrEndScore, Array arrAbsenceClass)
        {
            var strScoreID = arrScoreID.GetValue(0).ToString();
            var strDailyScore = arrDailyScore.GetValue(0).ToString();
            var strInterimScore = arrInterimScore.GetValue(0).ToString();
            var strEndScore = arrEndScore.GetValue(0).ToString();
            var strAbsenceClass = arrAbsenceClass.GetValue(0).ToString();

            var getScoreIDArr = strScoreID.Split(',');
            var getDailyScoreArr = strDailyScore.Split(',');         
            var getInterimScoreArr = strInterimScore.Split(',');   
            var getEndScoreArr = strEndScore.Split(',');     
            var getAbsenceClassArr = strAbsenceClass.Split(',');

            IScoreInfoMent ScoreMethod = new ScoreInfoMent();
            return Content(ScoreMethod.ScoreTemporarySave(getScoreIDArr, getDailyScoreArr, getInterimScoreArr, getEndScoreArr, getAbsenceClassArr));
        }

        //成绩管理列表的提交
        [HttpPost]
        public ActionResult ScoreRealSave(Array arrScoreID, Array arrDailyScore, Array arrInterimScore, Array arrEndScore, Array arrAbsenceClass)
        {
            var strScoreID = arrScoreID.GetValue(0).ToString();
            var strDailyScore = arrDailyScore.GetValue(0).ToString();
            var strInterimScore = arrInterimScore.GetValue(0).ToString();
            var strEndScore = arrEndScore.GetValue(0).ToString();
            var strAbsenceClass = arrAbsenceClass.GetValue(0).ToString();

            var getScoreIDArr = strScoreID.Split(',');
            var getDailyScoreArr = strDailyScore.Split(',');
            var getInterimScoreArr = strInterimScore.Split(',');
            var getEndScoreArr = strEndScore.Split(',');
            var getAbsenceClassArr = strAbsenceClass.Split(',');

            IScoreInfoMent ScoreMethod = new ScoreInfoMent();
            return Content(ScoreMethod.ScoreRealSave(getScoreIDArr, getDailyScoreArr, getInterimScoreArr, getEndScoreArr, getAbsenceClassArr));
        }

    }
}
