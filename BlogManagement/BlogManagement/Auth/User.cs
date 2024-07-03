using BlogManagement.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogManagement.Auth
{
    public class User:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = (UserInfo)httpContext.Session["user"];
            if (user!=null && user.Role.Equals("User"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}