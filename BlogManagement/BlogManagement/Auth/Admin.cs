using BlogManagement.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogManagement.Auth
{
    public class Admin:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var admin = (UserInfo)httpContext.Session["user"];
            if (admin != null && admin.Role.Equals("Admin")) {
            return true;
            }
            else {
                return false;    
            }
        }
    }
}