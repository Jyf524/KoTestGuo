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
    public class TeacherInfoMent : BaseRepository, ITeacherInfoMent
    {
        public TeacherInfoMentListDM TeacherInfoMentList(String TeacherName, String TeacherSex, int? page = 1)//教师列表
        {
            int SumNumber = 10;//每页几条数据
            TeacherInfoMentListDM model1 = new TeacherInfoMentListDM();


            var Teachers = (from m in db.TeacherRepository select m).ToList();

            if (!String.IsNullOrEmpty(TeacherName))
            {
                Teachers = Teachers.Where(s => s.TeacherName.Contains(TeacherName)).ToList();
            }
            if (!String.IsNullOrEmpty(TeacherSex))
            {
                Teachers = Teachers.Where(s => s.TeacherSex.Equals(TeacherSex)).ToList();
            }
            int CountPage = Teachers.Count();//总数据条数

            int AllPage = CountPage % SumNumber == 0 ? CountPage / SumNumber : CountPage / SumNumber + 1;//总页数
            Teachers = Teachers.OrderByDescending(x => x.CreationTime).Skip((page.Value - 1) * SumNumber).Take(SumNumber).ToList();
            model1.TeacherList = Teachers.ToList();
            model1.NowPage = page.Value;
            model1.AllPage = AllPage;
            model1.CountPage = CountPage;
            return model1;

        }

        public String TeacherInfoMentDelete(String TeacherID)//删除教师
        {
            Teacher TeacherInfo = db.TeacherRepository.Find(TeacherID);
            if (TeacherInfo == null)
            {
                return "删除失败！";
            }

            db.TeacherRepository.Remove(TeacherInfo);
            db.SaveChanges();
            return "删除成功！";
        }

        public TeacherInfoEditDM TeacherInfoEditGet(String TeacherID)//教师修改的Get方法
        {
            Teacher TeacherInfo = db.TeacherRepository.Find(TeacherID);
            TeacherInfoEditDM model1 = new TeacherInfoEditDM();
            if (TeacherInfo == null)
            {
                return null;
            }

            model1.TeacherID = TeacherInfo.TeacherID;
            model1.TeacherName = TeacherInfo.TeacherName;
            model1.TeacherSex = TeacherInfo.TeacherSex;
            model1.IdentityNumber = TeacherInfo.IdentityNumber;
            model1.CreationTime = TeacherInfo.CreationTime;

            return model1;
        }
        public String TeacherInfoEditPost(String TeacherID, String TeacherName, String TeacherSex, String IdentityNumber)//教师修改的Post方法
        {
            if (String.IsNullOrEmpty(TeacherName))
            {
                return "教师姓名不能为空！";
            }
            if (String.IsNullOrEmpty(TeacherSex))
            {
                return "请选择教师性别！";
            }
            if (String.IsNullOrEmpty(IdentityNumber))
            {
                return "身份证号不能为空！";
            }
            var TeacherInfos = (from a1 in db.TeacherRepository
                             where a1.TeacherID == (from a2 in db.TeacherRepository where a2.TeacherName == TeacherName select a2.TeacherID).FirstOrDefault()
                             where a1.TeacherID != TeacherID
                             select a1).Count();
            if (TeacherInfos > 0)
            {
                return "教师姓名已存在！";
            }

            Teacher TeacherInfo = new Teacher();
            TeacherInfo = db.TeacherRepository.Find(TeacherID);
            if (TeacherInfo == null)
            {
                return null;
            }
            TeacherInfo.TeacherName = TeacherName;
            TeacherInfo.TeacherSex = TeacherSex;
            TeacherInfo.IdentityNumber = IdentityNumber;
            db.SaveChanges();
            return "修改成功！";
        }

        public String TeacherInfoAdd(String TeacherName, String TeacherSex, String IdentityNumber)//添加教师
        {
            if (String.IsNullOrEmpty(TeacherName))
            {
                return "教师姓名不能为空！";
            }
            if (String.IsNullOrEmpty(TeacherSex))
            {
                return "请选择教师性别！";
            }
            if (String.IsNullOrEmpty(IdentityNumber))
            {
                return "身份证号不能为空！";
            }

            var TeacherInfos = (from a1 in db.TeacherRepository
                                where a1.TeacherID == (from a2 in db.TeacherRepository where a2.TeacherName == TeacherName select a2.TeacherID).FirstOrDefault()
                                select a1).Count();
            if (TeacherInfos > 0)
            {
                return "教师姓名已存在！";
            }

            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherID = DateTime.Now.ToString("yyyyMMddhhmmss");
            TeacherInfo.TeacherName = TeacherName;
            TeacherInfo.TeacherSex = TeacherSex;
            TeacherInfo.IdentityNumber = IdentityNumber;
            TeacherInfo.CreationTime = DateTime.Now;
            db.TeacherRepository.Add(TeacherInfo);
            db.SaveChanges();
            return "教师添加成功！";
        }
    }
}
