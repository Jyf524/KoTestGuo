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
    public class ClassInfoMent : BaseRepository, IClassInfoMent
    {
        public ClassInfoMentListDM ClassInfoMentList(String ClassName, String DepartmentName, int? page = 1)//班级列表
        {
            int SumNumber = 10;//每页几条数据
            ClassInfoMentListDM model1 = new ClassInfoMentListDM();


            var Classs = (from m in db.ClassRepository select m).ToList();

            if (!String.IsNullOrEmpty(ClassName))
            {
                Classs = Classs.Where(s => s.ClassName.Contains(ClassName)).ToList();
            }
            if (!DepartmentName.Equals("请选择"))
            {
                Classs = Classs.Where(s => s.DepartmentName.Contains(DepartmentName)).ToList();
            }
            int CountPage = Classs.Count();//总数据条数

            int AllPage = CountPage % SumNumber == 0 ? CountPage / SumNumber : CountPage / SumNumber + 1;//总页数
            Classs = Classs.OrderByDescending(x => x.CreationTime).Skip((page.Value - 1) * SumNumber).Take(SumNumber).ToList();
            model1.ClassList = Classs.ToList();
            model1.NowPage = page.Value;
            model1.AllPage = AllPage;
            model1.CountPage = CountPage;
            return model1;

        }
        public String ClassInfoMentDelete(String ClassID)//删除班级
        {
            Class ClassInfo = db.ClassRepository.Find(ClassID);
            if (ClassInfo == null)
            {
                return "删除失败！";
            }

            db.ClassRepository.Remove(ClassInfo);
            db.SaveChanges();
            return "删除成功！";
        }

        public ClassInfoEditDM ClassInfoEditGet(String ClassID)//班级修改的Get方法
        {
            Class ClassInfo = db.ClassRepository.Find(ClassID);
            ClassInfoEditDM model1 = new ClassInfoEditDM();
            if (ClassInfo == null)
            {
                return null;
            }
            model1.ClassID = ClassInfo.ClassID;
            model1.ClassName = ClassInfo.ClassName;
            model1.DepartmentName = ClassInfo.DepartmentName;
            model1.CreationTime = ClassInfo.CreationTime;

            return model1;
        }
        public String ClassInfoEditPost(String ClassID, String ClassName)//班级修改的Post方法
        {
            if (String.IsNullOrEmpty(ClassName))
            {
                return "班级名不能为空！";
            }
            var ClassInfos = (from a1 in db.ClassRepository
                              where a1.ClassID == (from a2 in db.ClassRepository where a2.ClassName == ClassName select a2.ClassID).FirstOrDefault()
                              where a1.ClassID != ClassID
                             select a1).Count();
            if (ClassInfos > 0)
            {
                return "班级名已存在！";
            }

            Class ClassInfo = new Class();
            ClassInfo = db.ClassRepository.Find(ClassID);
            if (ClassInfo == null)
            {
                return null;
            }
            ClassInfo.ClassName = ClassName;
            db.SaveChanges();
            return "修改成功！";
        }

        public String ClassInfoAdd(String ClassName, String DepartmentName, String MajorName)//添加班级
        {
            if (String.IsNullOrEmpty(ClassName))
            {
                return "班级名不能为空！";
            }
            if (DepartmentName.Equals("请选择"))
            {
                return "请先选择所属系部！";
            }
            if (String.IsNullOrEmpty(MajorName))
            {
                return "专业名称不能为空！";
            }
            var ClassInfos = (from a1 in db.ClassRepository
                              where a1.ClassID == (from a2 in db.ClassRepository where a2.ClassName == ClassName select a2.ClassID).FirstOrDefault()
                             select a1).Count();
            if (ClassInfos > 0)
            {
                return "班级名已存在！";
            }

            Class ClassInfo = new Class();
            ClassInfo.ClassID = DateTime.Now.ToString("yyyyMMddhhmmss");
            ClassInfo.ClassName = ClassName;
            ClassInfo.DepartmentName = DepartmentName;
            ClassInfo.MajorName = MajorName;
            ClassInfo.CreationTime = DateTime.Now;
            db.ClassRepository.Add(ClassInfo);
            db.SaveChanges();
            return "班级添加成功！";
        }
    }
}
