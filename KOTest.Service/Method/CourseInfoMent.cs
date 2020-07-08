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
    public class CourseInfoMent : BaseRepository, ICourseInfoMent
    {
        public CourseInfoMentListDM CourseInfoMentList(String SearchIngTxt, String CourseKind, String DepartmentName, int? page = 1)//课程列表
        {
            int SumNumber = 10;//每页几条数据
            CourseInfoMentListDM model1 = new CourseInfoMentListDM();


            var Courses = (from m in db.CourseRepository select m).ToList();

            if (!String.IsNullOrEmpty(SearchIngTxt))
            {
                Courses = Courses.Where(s => s.CourseNumber.Contains(SearchIngTxt) || s.CourseName.Contains(SearchIngTxt) || s.FullName.Contains(SearchIngTxt)).ToList();
            }
            if (!String.IsNullOrEmpty(CourseKind))
            {
                Courses = Courses.Where(s => s.CourseKind.Contains(CourseKind)).ToList();
            }
            if (!String.IsNullOrEmpty(DepartmentName))
            {
                Courses = Courses.Where(s => s.DepartmentName.Contains(DepartmentName)).ToList();
            }
            int CountPage = Courses.Count();//总数据条数

            int AllPage = CountPage % SumNumber == 0 ? CountPage / SumNumber : CountPage / SumNumber + 1;//总页数
            Courses = Courses.OrderByDescending(x => x.CreationTime).Skip((page.Value - 1) * SumNumber).Take(SumNumber).ToList();
            model1.CourseList = Courses.ToList();
            model1.NowPage = page.Value;
            model1.AllPage = AllPage;
            model1.CountPage = CountPage;
            return model1;

        }

        public String CourseInfoMentDelete(String CourseID)//删除课程
        {
            Course CourseInfo = db.CourseRepository.Find(CourseID);
            if (CourseInfo == null)
            {
                return "删除失败！";
            }

            db.CourseRepository.Remove(CourseInfo);
            db.SaveChanges();
            return "删除成功！";
        }

        public CourseInfoEditDM CourseInfoEditGet(String CourseID)//课程修改的Get方法
        {
            Course CourseInfo = db.CourseRepository.Find(CourseID);
            CourseInfoEditDM model1 = new CourseInfoEditDM();
            if (CourseInfo == null)
            {
                return null;
            }
            model1.CourseID = CourseInfo.CourseID;
            model1.CourseNumber = CourseInfo.CourseNumber;
            model1.CourseName = CourseInfo.CourseName;
            model1.FullName = CourseInfo.FullName;
            model1.FirstSpelling = CourseInfo.FirstSpelling;
            model1.AllSpelling = CourseInfo.AllSpelling;
            model1.CourseKind = CourseInfo.CourseKind;
            model1.DepartmentName = CourseInfo.DepartmentName;
            model1.Introduction = CourseInfo.Introduction;
            model1.CreationTime = CourseInfo.CreationTime;

            return model1;
        }
        public String CourseInfoEditPost(String CourseID,String CourseNumber, String CourseName, String FullName, String FirstSpelling, String AllSpelling, String CourseKind, String DepartmentName, String Introduction)//课程修改的Post方法
        {
            if (String.IsNullOrEmpty(CourseNumber))
            {
                return "编号不能为空！";
            }
            if (String.IsNullOrEmpty(CourseName))
            {
                return "名称不能为空！";
            }
            if (String.IsNullOrEmpty(FullName))
            {
                return "全称不能为空！";
            }
            if (String.IsNullOrEmpty(FirstSpelling))
            {
                return "首拼不能为空！";
            }
            if (String.IsNullOrEmpty(AllSpelling))
            {
                return "全拼不能为空！";
            }
            if (!CourseKind.Equals("请选择"))
            {
                return "请选择类型！";
            }
             
          var CourseInfos = (from a1 in db.CourseRepository
                               where a1.CourseID == (from a2 in db.CourseRepository where a2.CourseName == CourseName select a2.CourseID).FirstOrDefault()
                             where a1.CourseNumber != CourseNumber
                             select a1).Count();
            if (CourseInfos > 0)
            {
                return "编号已存在！";
            }

            Course CourseInfo = new Course();
            CourseInfo = db.CourseRepository.Find(CourseID);
            if (CourseInfo == null)
            {
                return null;
            }
            CourseInfo.CourseNumber = CourseNumber;
            CourseInfo.CourseName = CourseName;
            CourseInfo.FullName = FullName;
            CourseInfo.FirstSpelling = FirstSpelling;
            CourseInfo.AllSpelling = AllSpelling;
            CourseInfo.CourseKind = CourseKind;
            CourseInfo.DepartmentName = DepartmentName;
            CourseInfo.Introduction = Introduction;
            db.SaveChanges();
            return "修改成功！";
        }

        //添加课程
        public String CourseInfoAdd(String CourseImg,String CourseNumber, String CourseName, String FullName, String FirstSpelling,String AllSpelling, String CourseKind, String DepartmentName, String Introduction)
        {
            if (String.IsNullOrEmpty(CourseNumber))
            {
                return "编号不能为空！";
            }
            if (String.IsNullOrEmpty(CourseName))
            {
                return "名称不能为空！";
            }
            if (String.IsNullOrEmpty(FullName))
            {
                return "全称不能为空！";
            }
            if (String.IsNullOrEmpty(FirstSpelling))
            {
                return "首拼不能为空！";
            }
            if (String.IsNullOrEmpty(AllSpelling))
            {
                return "全拼不能为空！";
            }
            if (CourseKind.Equals("请选择"))
            {
                return "请选择类型！";
            }
            var CourseInfos = (from a1 in db.CourseRepository
                               where a1.CourseID == (from a2 in db.CourseRepository where a2.CourseName == CourseName select a2.CourseID).FirstOrDefault()
                             select a1).Count();
            if (CourseInfos > 0)
            {
                return "编号已存在！";
            }

            Course CourseInfo = new Course();
            CourseInfo.CourseID = DateTime.Now.ToString("yyyyMMddhhmmss");
            CourseInfo.CourseNumber = CourseNumber;
            CourseInfo.CourseName = CourseName;
            CourseInfo.CourseImg = CourseImg;
            CourseInfo.FullName = FullName;
            CourseInfo.FirstSpelling = FirstSpelling;
            CourseInfo.AllSpelling = AllSpelling;
            CourseInfo.CourseKind = CourseKind;
            CourseInfo.DepartmentName = DepartmentName;
            CourseInfo.Introduction = Introduction;
            CourseInfo.CreationTime = DateTime.Now;
            db.CourseRepository.Add(CourseInfo);
            db.SaveChanges();
            CourseInfoAddDM.Url = "";
            return "课程添加成功！";
        }
    }
}
