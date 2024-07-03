using AutoMapper;
using BlogManagement.Auth;
using BlogManagement.DTOs;
using BlogManagement.EF;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace BlogManagement.Controllers
{
    [User]
    public class UserController : Controller
    {
        BlogManagementDbEntities db = new BlogManagementDbEntities();
        // GET: User
        public ActionResult Index()
        {
            var list=db.PostInfoes.ToList();
            if (list != null) {
                var clist = AdminController.Convert(list);
                return View(clist);
            }
            TempData["Msg"] = "No Post Found";
            return View(new PostDTO());
        }
        [HttpGet]
        public ActionResult ViewPost(int id) {
            var postDetails = (from post in db.PostInfoes
                               where post.PostId == id
                               select new
                               {
                                   post.PostId,
                                   post.PostTitle,
                                   post.PostBody,
                                   post.PostDate,
                                   Comments = (from com in db.CommentInfoes
                                               where com.PostId == id
                                               join user in db.UserInfoes on com.CommentUser equals user.UserId
                                               select new CommentDTO
                                               {
                                                   CommentId = com.CommentId,
                                                   CommentDetails = com.CommentDetails,
                                                   UserName = user.UserName,
                                                   CommentTime = com.CommentTime
                                               }).ToList()
                               }).FirstOrDefault();
            var listDTO = new ListDTO
            {
                PostId = postDetails.PostId,
                PostTitle = postDetails.PostTitle,
                PostBody = postDetails.PostBody,
                Comments = postDetails.Comments
            };
            return View(listDTO);

        }
        [HttpPost]
        public ActionResult ViewPost(CommentDTO c) {
            c.CommentUser = ((UserInfo)Session["user"]).UserId;
            if (ModelState.IsValid) {
                var f = Convert(c);
                db.CommentInfoes.Add(f);
                db.SaveChanges();
                TempData["Msg"] = "Comment Posted";
                return RedirectToAction("Index");
            }
            return View(c);
        }

        public static CommentInfo Convert(CommentDTO c) {
            return new CommentInfo
            {
                CommentDetails = c.CommentDetails,
                CommentTime = DateTime.Now,
                CommentUser = c.CommentUser,
                PostId = c.PostId
            };
        }
        public ActionResult Logout() {
            Session["user"] = null;
            TempData["Msg"] = "Logout successful";
        return RedirectToAction("Index","Home");
        }

    }
}