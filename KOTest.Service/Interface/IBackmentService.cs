using KOTest.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Interface
{
    public interface IBackmentService
    {
        String Login(String UserName, String Password, String LoginCode, String ConfirmCode);//登录
        AdminerRoleDM LoginRole(String UserName, String Password);//登录判断身份传入session
    }
}
