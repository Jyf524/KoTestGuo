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
    public class ChoiceCourseMent : BaseRepository, IChoiceCourseMent
    {
        public ChoiceCourseListDM ChoiceCourseList(String CourseName, String ClassName, int? page = 1)//选课管理的列表
        {
            var messages = (from a1 in db.ChoiceCourseRepository
                            select new ChoiceCourseList
                            {
                                ChoiceCourseID = a1.ChoiceCourseID,
                                ClassName = a1.ClassName,
                                CourseName = a1.CourseName,
                                CourseTeacherID = a1.CourseTeacherID,
                                CourseTeacherName = a1.CourseTeacherName,
                                CreationTime = a1.CreationTime
                            }).ToList();//寻找当前id的数据

            if (!String.IsNullOrEmpty(CourseName))
            {
                messages = messages.Where(s => s.CourseName.Contains(CourseName)).ToList();
            }
            if (!ClassName.Equals("-1"))
            {
                messages = messages.Where(s => s.ClassName.Equals(ClassName)).ToList();
            }

            int SumNumber = 10;//每页几条数据
            ChoiceCourseListDM model1 = new ChoiceCourseListDM();

            int CountPage = messages.Count();//总数据条数
            int AllPage = CountPage % SumNumber == 0 ? CountPage / SumNumber : CountPage / SumNumber + 1;//总页数
            messages = messages.OrderByDescending(x => x.CreationTime).ToList();
            model1.ChoiceCourseList = messages.ToList();
            model1.NowPage = page.Value;
            model1.AllPage = AllPage;
            model1.CountPage = CountPage;
            return model1;
        }

        public ClassNavListDM ClassNavList()//课程导航条
        {
            ClassNavListDM model1 = new ClassNavListDM();
            var Classs = (from m in db.ClassRepository select m).ToList();
            int CountPage = Classs.Count();//总数据条数
            Classs = Classs.OrderByDescending(x => x.CreationTime).ToList();
            model1.ClassList= Classs.ToList();
            model1.CountPage = CountPage;
            return model1;
        }

        public String ChoiceCourseAdd(String ClassName, String CourseName, String WeekClassHours, String IsnotExamination, String CourseKind)//添加选课
        {
            if (String.IsNullOrEmpty(ClassName))
            {
                return "班级不能为空！";
            }
            if (String.IsNullOrEmpty(CourseName))
            {
                return "课程不能为空！";
            }
            if (String.IsNullOrEmpty(WeekClassHours))
            {
                return "周学时不能为空！";
            }
            if (IsnotExamination.Equals("请选择"))
            {
                return "请选择是否考试！";
            }
            if (CourseKind.Equals("请选择"))
            {
                return "请选择课程类型！";
            }

            ChoiceCourse ChoiceCourseInfo = new ChoiceCourse();
            ChoiceCourseInfo.ChoiceCourseID = DateTime.Now.ToString("yyyyMMddhhmmss");
            ChoiceCourseInfo.ClassName = ClassName;
            ChoiceCourseInfo.CourseName = CourseName;
            ChoiceCourseInfo.WeekClassHours = WeekClassHours;
            ChoiceCourseInfo.IsnotExamination = IsnotExamination;
            ChoiceCourseInfo.CourseKind = CourseKind;
            ChoiceCourseInfo.CreationTime = DateTime.Now;
            db.ChoiceCourseRepository.Add(ChoiceCourseInfo);
            db.SaveChanges();
            return "选课添加成功！";
        }

        public CourseTeacherListDM CourseTeacherList()//分配老师列表
        {
            CourseTeacherListDM model1 = new CourseTeacherListDM();
            var Teachers = (from m in db.TeacherRepository select m).ToList();
            int CountPage = Teachers.Count();//总数据条数
            Teachers = Teachers.OrderByDescending(x => x.CreationTime).ToList();
            model1.CourseTeacherList = Teachers.ToList();
            model1.CountPage = CountPage;
            return model1;
        }

        //分配老师
        public String CourseTeacherAdopt(String ChoiceCourseID,Array CourseTeacherID)
        {

            string memberName = "";//指导老师名称
            string RealmemberID = "";//指导老师编号
            for (var i = 0; i < CourseTeacherID.Length; i++)
            {
                var TeacherID = CourseTeacherID.GetValue(i);
                Teacher Customer = db.TeacherRepository.Find(TeacherID);
                if (Customer != null)
                {
                    if (i == CourseTeacherID.Length - 1)
                    {
                        memberName += String.Format("{0}", Customer.TeacherName);
                        RealmemberID += String.Format("{0}", Customer.TeacherID);
                    }
                    else
                    {
                        memberName += String.Format("{0}{1}", Customer.TeacherName, ",");
                        RealmemberID += String.Format("{0}{1}", Customer.TeacherID, ",");
                    }

                }
            }

            ChoiceCourse ChoiceCourseInfo = new ChoiceCourse();
            ChoiceCourseInfo = db.ChoiceCourseRepository.Find(ChoiceCourseID);
            if (ChoiceCourseInfo == null)
            {
                return null;
            }
            ChoiceCourseInfo.CourseTeacherID = RealmemberID;
            ChoiceCourseInfo.CourseTeacherName = memberName;
            db.SaveChanges();
            return "修改成功！";
        }
    }
}
