using BlogManagement.DTOs;
using BlogManagement.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogManagement.Controllers
{
    public class HomeController : Controller
    {
        BlogManagementDbEntities db = new BlogManagementDbEntities();
        [HttpGet]
        public ActionResult Index()
        {
            return View(new LoginDTO());
        }

        [HttpPost]
        public ActionResult Index(LoginDTO l)
        {
            if (ModelState.IsValid) {
                var login = (from u in db.UserInfoes
                             where u.UserName.Equals(l.UserName) &&
                             u.Password.Equals(l.Password)
                             select u).SingleOrDefault();
                if (login == null)
                {
                    TempData["Msg"] = "Invalid login credential";
                    return View(l);
                }
                else {
                    Session["User"] = login;
                    TempData["Msg"] = "Login Successfull";
                    if (login.Role.Equals("User"))
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else if(login.Role.Equals("Admin")){
                        return RedirectToAction("Index", "Admin");
                    }
                }
            }
            return View(l);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View(new UserDTO());
        }
        [HttpPost]
        public ActionResult Registration(UserDTO u)
        {
            if (ModelState.IsValid) {
                var user = Convert(u);
                db.UserInfoes.Add(user);
                db.SaveChanges();
                TempData["Msg"] = "New user added";
                return RedirectToAction("Index");

            }
            return View(u);
        }

        public static UserInfo Convert(UserDTO u) {
            return new UserInfo
            {
                FullName = u.FirstName.Trim() + " " + u.LastName.Trim(),
                Email = u.Email,
                UserName = u.UserName,
                Phone = u.Phone,
                Password = u.Password,
                Role = "User",
                Status="Active"
            };
        }

        public static UserDTO Convert(UserInfo u) {
            var name = u.FullName.Split(' ');
            return new UserDTO
            {
                UserId = u.UserId,
                FirstName = name[0],
                LastName = name[1],
                Email = u.Email,
                UserName = u.UserName,
                Phone = u.Phone,
                Password = u.Password,
                Role = u.Role,
                Status = u.Status
            };
        }
    }
}