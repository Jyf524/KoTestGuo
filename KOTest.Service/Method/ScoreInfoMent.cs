using KOTest.Repository;
using KOTest.Repository.Entities;
using KOTest.Service.Interface;
using KOTest.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Method
{
    public class ScoreInfoMent : BaseRepository, IScoreInfoMent
    {
        public ScoreListDM ScoreList(String UsersName, String CourseName, int? page = 1)//成绩管理的列表
        {
            var Scores = (from a1 in db.ScoreRepository
                            join a2 in db.UsersRepository on a1.UsersID equals a2.UsersID
                            join a3 in db.CourseRepository on a1.CourseID equals a3.CourseID
                            select new ScoreList
                            {
                                ScoreID = a1.ScoreID,
                                UsersID = a1.UsersID,
                                CourseID = a1.CourseID,
                                DailyScore = a1.DailyScore,
                                InterimScore = a1.InterimScore,
                                EndScore = a1.EndScore,
                                AbsenceClass = a1.AbsenceClass,
                                AllComments = a1.AllComments,
                                ScoreState = a1.ScoreState,
                                CreationTime = a1.CreationTime,

                                UsersName = a2.UsersName,
                                IdentityNumber = a2.IdentityNumber,

                                CourseName = a3.CourseName
                            }).ToList();//寻找当前id的数据

            if (!String.IsNullOrEmpty(UsersName))
            {
                Scores = Scores.Where(s => s.UsersName.Contains(UsersName)).ToList();
            }
            if (!String.IsNullOrEmpty(CourseName))
            {
                Scores = Scores.Where(s => s.CourseName.Contains(CourseName)).ToList();
            }

            int SumNumber = 10;//每页几条数据
            ScoreListDM model1 = new ScoreListDM();

            int CountPage = Scores.Count();//总数据条数
            int AllPage = CountPage % SumNumber == 0 ? CountPage / SumNumber : CountPage / SumNumber + 1;//总页数
            Scores = Scores.OrderByDescending(x => x.CreationTime).ToList();
            model1.ScoreList = Scores.ToList();
            model1.NowPage = page.Value;
            model1.AllPage = AllPage;
            model1.CountPage = CountPage;
            return model1;
        }

        public String ScoreTemporarySave(Array ScoreID, Array DailyScore, Array InterimScore, Array EndScore, Array AbsenceClass)//成绩管理列表的暂存
        {
            //if (String.IsNullOrEmpty(DailyScore))
            //{
            //    return "平时成绩不能为空！";
            //}
            //if (String.IsNullOrEmpty(InterimScore))
            //{
            //    return "期中成绩不能为空！";
            //}
            //if (String.IsNullOrEmpty(EndScore))
            //{
            //    return "期末成绩不能为空！";
            //}
            //if (String.IsNullOrEmpty(AbsenceClass))
            //{
            //    return "缺勤课时数不能为空！";
            //}

            //Score ScoreInfo = new Score();
            //ScoreInfo = db.ScoreRepository.Find(ScoreID);
            //if (ScoreInfo == null)
            //{
            //    return null;
            //}
            //ScoreInfo.DailyScore = DailyScore;
            //ScoreInfo.InterimScore = InterimScore;
            //ScoreInfo.EndScore = EndScore;
            //ScoreInfo.AbsenceClass = AbsenceClass;
            //ScoreInfo.AllComments = (Convert.ToInt32(DailyScore) * 0.3 + Convert.ToInt32(InterimScore) * 0.3 + Convert.ToInt32(EndScore) * 0.3).ToString();
            //db.SaveChanges();


            for (var i = 0; i < ScoreID.Length; i++)
            {
                var ScoreIDVal = ScoreID.GetValue(i);
                Score ScoreInfo = new Score();
                ScoreInfo = db.ScoreRepository.Find(ScoreIDVal);

                if (ScoreInfo.ScoreState!= "Finish")
                {
                    ScoreInfo.DailyScore = DailyScore.GetValue(i).ToString();
                    ScoreInfo.InterimScore = InterimScore.GetValue(i).ToString();
                    ScoreInfo.EndScore = EndScore.GetValue(i).ToString();
                    ScoreInfo.AbsenceClass = AbsenceClass.GetValue(i).ToString();
                    ScoreInfo.AllComments = (Convert.ToInt32(DailyScore.GetValue(i).ToString()) * 0.3 + Convert.ToInt32(InterimScore.GetValue(i).ToString()) * 0.3 + Convert.ToInt32(EndScore.GetValue(i).ToString()) * 0.3).ToString();
                    db.SaveChanges();
                }
            }

            return "暂存成功！";
        }

        public String ScoreRealSave(Array ScoreID, Array DailyScore, Array InterimScore, Array EndScore, Array AbsenceClass)//成绩管理列表的提交
        {
            for (var i = 0; i < ScoreID.Length; i++)
            {
                var ScoreIDVal = ScoreID.GetValue(i);
                Score ScoreInfo = new Score();
                ScoreInfo = db.ScoreRepository.Find(ScoreIDVal);

                if (ScoreInfo.ScoreState != "Finish")
                {
                    ScoreInfo.DailyScore = DailyScore.GetValue(i).ToString();
                    ScoreInfo.InterimScore = InterimScore.GetValue(i).ToString();
                    ScoreInfo.EndScore = EndScore.GetValue(i).ToString();
                    ScoreInfo.AbsenceClass = AbsenceClass.GetValue(i).ToString();
                    ScoreInfo.AllComments = (Convert.ToInt32(DailyScore.GetValue(i).ToString()) * 0.3 + Convert.ToInt32(InterimScore.GetValue(i).ToString()) * 0.3 + Convert.ToInt32(EndScore.GetValue(i).ToString()) * 0.3).ToString();
                    ScoreInfo.ScoreState = "Finish";
                    db.SaveChanges();
                }
            }
            return "提交成功！";
        }
    }
}
