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
    public class SemesterInfoMent : BaseRepository, ISemesterInfoMent
    {
        public SemesterInfoMentListDM SemesterInfoMentList(String SemesterName, String SemesterState, int? page = 1)//学期列表
        {
            int SumNumber = 10;//每页几条数据
            SemesterInfoMentListDM model1 = new SemesterInfoMentListDM();


            var Semesters = (from m in db.SemesterRepository select m).ToList();

            if (!String.IsNullOrEmpty(SemesterName))
            {
                Semesters = Semesters.Where(s => s.SemesterName.Contains(SemesterName)).ToList();
            }
            if (!SemesterState.Equals("请选择"))
            {
                Semesters = Semesters.Where(s => s.SemesterState.Contains(SemesterState)).ToList();
            }
            int CountPage = Semesters.Count();//总数据条数

            int AllPage = CountPage % SumNumber == 0 ? CountPage / SumNumber : CountPage / SumNumber + 1;//总页数
            Semesters = Semesters.OrderByDescending(x => x.CreationTime).Skip((page.Value - 1) * SumNumber).Take(SumNumber).ToList();
            model1.SemesterList = Semesters.ToList();
            model1.NowPage = page.Value;
            model1.AllPage = AllPage;
            model1.CountPage = CountPage;
            return model1;

        }
        public String SemesterInfoMentDelete(String SemesterID)//删除学期
        {
            Semester SemesterInfo = db.SemesterRepository.Find(SemesterID);
            if (SemesterInfo == null)
            {
                return "删除失败！";
            }

            db.SemesterRepository.Remove(SemesterInfo);
            db.SaveChanges();
            return "删除成功！";
        }

        public SemesterInfoEditDM SemesterInfoEditGet(String SemesterID)//学期修改的Get方法
        {
            Semester SemesterInfo = db.SemesterRepository.Find(SemesterID);
            SemesterInfoEditDM model1 = new SemesterInfoEditDM();
            if (SemesterInfo == null)
            {
                return null;
            }
            model1.SemesterID = SemesterInfo.SemesterID;
            model1.SemesterName = SemesterInfo.SemesterName;
            model1.SemesterState = SemesterInfo.SemesterState;
            model1.CreationTime = SemesterInfo.CreationTime;

            return model1;
        }
        public String SemesterInfoEditPost(String SemesterID, String SemesterName)//学期修改的Post方法
        {
            if (String.IsNullOrEmpty(SemesterName))
            {
                return "学期名不能为空！";
            }
            var SemesterInfos = (from a1 in db.SemesterRepository
                              where a1.SemesterID == (from a2 in db.SemesterRepository where a2.SemesterName == SemesterName select a2.SemesterID).FirstOrDefault()
                              where a1.SemesterID != SemesterID
                              select a1).Count();
            if (SemesterInfos > 0)
            {
                return "学期名已存在！";
            }

            Semester SemesterInfo = new Semester();
            SemesterInfo = db.SemesterRepository.Find(SemesterID);
            if (SemesterInfo == null)
            {
                return null;
            }
            SemesterInfo.SemesterName = SemesterName;
            db.SaveChanges();
            return "修改成功！";
        }

        public String SemesterInfoAdd(String SemesterName, String SemesterState)//添加学期
        {
            if (String.IsNullOrEmpty(SemesterName))
            {
                return "学期名不能为空！";
            }
            if (SemesterState.Equals("请选择"))
            {
                return "请先选择学期状态！";
            }

            var SemesterInfos = (from a1 in db.SemesterRepository
                              where a1.SemesterID == (from a2 in db.SemesterRepository where a2.SemesterName == SemesterName select a2.SemesterID).FirstOrDefault()
                              select a1).Count();
            if (SemesterInfos > 0)
            {
                return "学期名已存在！";
            }

            Semester SemesterInfo = new Semester();
            SemesterInfo.SemesterID = DateTime.Now.ToString("yyyyMMddhhmmss");
            SemesterInfo.SemesterName = SemesterName;
            SemesterInfo.SemesterState = SemesterState;
            SemesterInfo.CreationTime = DateTime.Now;
            db.SemesterRepository.Add(SemesterInfo);
            db.SaveChanges();
            return "学期添加成功！";
        }
    }
}
