using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeMent.Util
{
    public class UserInfo
    {
        public static String UserID//可以用linq替代
        {
            get
            {
                object obj = HttpContext.Current.Session["_USERID"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set
            {
                HttpContext.Current.Session["_USERID"] = value;
            }
        }

        public static String UserRole
        {
            get
            {
                object obj = HttpContext.Current.Session["_USERROLE"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set
            {
                HttpContext.Current.Session["_USERROLE"] = value;
            }
        }

        public static String UserName
        {
            get
            {
                object obj = HttpContext.Current.Session["_UserName"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set
            {
                HttpContext.Current.Session["_UserName"] = value;
            }
        }
    }
}