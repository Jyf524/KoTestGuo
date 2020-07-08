using KOTest.Repository;
using KOTest.Service.Interface;
using KOTest.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Method
{
    public class BackmentService : BaseRepository, IBackmentService
    {
        public String Login(String UserName, String Password, String LoginCode, String ConfirmCode)//登录
        {

            if (string.IsNullOrEmpty(UserName))
            {

                return "用户名不能为空！";
            }
            if (string.IsNullOrEmpty(Password))
            {

                return "密码不能为空！";
            }
            if (string.IsNullOrEmpty(LoginCode))
            {

                return "验证码不能为空！";
            }
            if (ConfirmCode != LoginCode.ToLower())
            {
                return "请输入正确的验证码！";
            }

            var Verification = (from a1 in db.UsersRepository
                                where a1.UsersID == (from a2 in db.UsersRepository where a2.UsersName == UserName select a2.UsersID).FirstOrDefault()
                                && a1.Password == Password
                                select a1).Count();

            if (Verification > 0)
            {

                return "登录成功！";
                //登录成功
            }
            else
            {
                return "用户名或账号错误！";
                //登录失败
            }
        }
        public AdminerRoleDM LoginRole(String UserName, String Password)//登录判断身份传入session
        {

            AdminerRoleDM model1 = new AdminerRoleDM();
            //string Role = db.UsersRepository.Where(x => x.UsersName == UserName && x.Password == Password).ToList().First().UserRole;
            string ID = db.UsersRepository.Where(x => x.UsersName == UserName && x.Password == Password).ToList().First().UsersID;
            //model1.UserRole = Role;
            model1.UserID = ID;
            return model1;
        }
    }
}
