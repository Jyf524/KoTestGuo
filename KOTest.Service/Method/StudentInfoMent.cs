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
    public class StudentInfoMent : BaseRepository, IStudentInfoMent
    {
        public StudentInfoMentListDM StudentInfoMentList(String UsersName, String UsersSex, int? page = 1)//学生列表
        {
            int SumNumber = 10;//每页几条数据
            StudentInfoMentListDM model1 = new StudentInfoMentListDM();


            var Students = (from m in db.UsersRepository select m).ToList();

            if (!String.IsNullOrEmpty(UsersName))
            {
                Students = Students.Where(s => s.UsersName.Contains(UsersName)).ToList();
            }
            if (!String.IsNullOrEmpty(UsersSex))
            {
                Students = Students.Where(s => s.UsersSex.Equals(UsersSex)).ToList();
            }
            int CountPage = Students.Count();//总数据条数

            int AllPage = CountPage % SumNumber == 0 ? CountPage / SumNumber : CountPage / SumNumber + 1;//总页数
            Students = Students.OrderByDescending(x => x.CreationTime).Skip((page.Value - 1) * SumNumber).Take(SumNumber).ToList();
            model1.UsersList = Students.ToList();
            model1.NowPage = page.Value;
            model1.AllPage = AllPage;
            model1.CountPage = CountPage;
            return model1;

        }
        //public String StudentInfoMentDelete(String UsersID)//删除学生
        //{
        //    Users UserInfo = db.UsersRepository.Find(UsersID);
        //    if (UserInfo == null)
        //    {
        //        return "删除失败！";
        //    }

        //    db.UsersRepository.Remove(UserInfo);
        //    db.SaveChanges();
        //    return "删除成功！";
        //}

        public String StudentInfoMentDelete(Array arr)//删除学生
        {
            if (arr.Length == 0)//判断是否有选择学生
            {
                return "请先选择学生";
            }

            for (var i = 0; i < arr.Length; i++)
            {
                var StudentID = arr.GetValue(i);
                Users UserInfo = db.UsersRepository.Find(StudentID);
                if (StudentID != null)
                {
                    db.UsersRepository.Remove(UserInfo);
                    
                }
            }

            db.SaveChanges();
            return "删除成功！";
        }

        public StudentInfoEditDM StudentInfoEditGet(String UsersID)//学生修改的Get方法
        {
            Users UserInfo = db.UsersRepository.Find(UsersID);
            StudentInfoEditDM model1 = new StudentInfoEditDM();
            if (UserInfo == null)
            {
                return null;
            }
            model1.UsersID = UserInfo.UsersID;
            model1.UsersWorkID = UserInfo.UsersWorkID;
            model1.UsersName = UserInfo.UsersName;
            model1.UsersSex = UserInfo.UsersSex;
            model1.UsersAge = UserInfo.UsersAge;
            model1.CreationTime = UserInfo.CreationTime;

            return model1;
        }
        public String StudentInfoEditPost(String UsersID, String UsersName)//学生修改的Post方法
        {
            if (String.IsNullOrEmpty(UsersName))
            {
                return "用户名不能为空！";
            }
            var UserInfos = (from a1 in db.UsersRepository
                             where a1.UsersID == (from a2 in db.UsersRepository where a2.UsersName == UsersName select a2.UsersID).FirstOrDefault()
                             where a1.UsersID != UsersID
                             select a1).Count();
            if (UserInfos > 0)
            {
                return "用户名已存在！";
            }

            Users UserInfo = new Users();
            UserInfo = db.UsersRepository.Find(UsersID);
            if (UserInfo == null)
            {
                return null;
            }
            UserInfo.UsersName = UsersName;
            db.SaveChanges();
            return "修改成功！";
        }

        public String StudentInfoAdd(String UsersWorkID, String UsersName, String Password,String UsersSex, String UsersAge)//添加学生
        {
            if (String.IsNullOrEmpty(UsersWorkID))
            {
                return "学号不能为空！";
            }
            if (String.IsNullOrEmpty(UsersName))
            {
                return "用户名不能为空！";
            }
            if (String.IsNullOrEmpty(Password))
            {
                return "学生密码不能为空！";
            }
            if (String.IsNullOrEmpty(UsersSex))
            {
                return "请选择性别！";
            }
            if (String.IsNullOrEmpty(UsersAge))
            {
                return "年龄不能为空！";
            }

            var UserInfos = (from a1 in db.UsersRepository
                             where a1.UsersID == (from a2 in db.UsersRepository where a2.UsersName == UsersName select a2.UsersID).FirstOrDefault()
                             select a1).Count();
            if (UserInfos > 0)
            {
                return "用户名已存在！";
            }

            Users UserInfo = new Users();
            UserInfo.UsersID = DateTime.Now.ToString("yyyyMMddhhmmss");
            UserInfo.UsersWorkID = UsersWorkID;
            UserInfo.UsersName = UsersName;
            UserInfo.Password = Password;
            UserInfo.UsersSex = UsersSex;
            UserInfo.UsersAge = UsersAge;
            UserInfo.CreationTime = DateTime.Now;
            db.UsersRepository.Add(UserInfo);
            db.SaveChanges();
            return "用户添加成功！";
        }

        public StudentExportListDM StudentExportList()//导出学生数据
        {

            StudentExportListDM model1 = new StudentExportListDM();

            var Students = (from m in db.UsersRepository select m).ToList();
            int CountPage = Students.Count();//总数据
            Students = Students.OrderByDescending(x => x.CreationTime).ToList();
            model1.UsersList = Students.ToList();
            model1.CountPage = CountPage;
            return model1;

        }
    }
}
