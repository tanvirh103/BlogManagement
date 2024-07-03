using AutoMapper;
using BlogManagement.Auth;
using BlogManagement.DTOs;
using BlogManagement.EF;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogManagement.Controllers
{
    [Admin]
    public class AdminController : Controller
    {
        // GET: Admin
        BlogManagementDbEntities db = new BlogManagementDbEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreatePost() {
            return View(new PostDTO());
        }
        [HttpPost]
        public ActionResult CreatePost(PostDTO p) {
            p.UserId = ((UserInfo)Session["user"]).UserId;
            if (ModelState.IsValid) {
                var post = Convert(p);
                db.PostInfoes.Add(post);
                db.SaveChanges();
                TempData["Msg"] = "New post added";
                return RedirectToAction("Index");
            }
            return View(p);
        }
        [HttpGet]
        public ActionResult ViewAllPost() {
            var post=db.PostInfoes.ToList();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<PostInfo, PostDTO>();
            });
            var mapper = new Mapper(config);
            var data = mapper.Map<List<PostDTO>>(post);
            return View(data);
        }
       
        [HttpGet]
        public ActionResult ViewAllUser() {
           var list= db.UserInfoes.ToList();
            var cuser = Convert(list);
            return View(cuser);
        }

        [HttpGet]
        public ActionResult EditPost(int id) {            
            var find = db.PostInfoes.Find(id);
            var post=Convert(find);
            return View(find);
        }
        [HttpPost]
        public ActionResult EditPost(PostDTO p) {
            if (ModelState.IsValid) {
                var find = db.PostInfoes.Find(p.PostId);
                find.PostBody = p.PostBody;
                find.PostTitle = p.PostTitle;
                find.PostStatus = "Edited";
                db.SaveChanges();
                TempData["Msg"] = "Post Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(p);
        }

        [HttpGet]
        public ActionResult DeletePost(int id) {
            var find=db.PostInfoes.Find(id);
            db.PostInfoes.Remove(find);
            db.SaveChanges();
            TempData["Msg"] = "Post Deleted";
            return RedirectToAction("Index");
        }

        public static PostDTO Convert(PostInfo p) {
            return new PostDTO
            {
                PostId=p.PostId,
                PostTitle = p.PostTitle,
                PostBody = p.PostBody,
                PostDate = p.PostDate,
                PostStatus=p.PostStatus
            };
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session["user"] = null;
            TempData["Msg"] = "Logout successful";
            return RedirectToAction("Index","Home");
        }

        public static PostInfo Convert(PostDTO p) {
            return new PostInfo
            {
                UserId = p.UserId,
                PostTitle=p.PostTitle,
                PostBody=p.PostBody,
                PostDate = DateTime.Now,
                PostStatus="Active"

            };
        }

        public static List<PostDTO> Convert(List<PostInfo> Posts) {
            var list=new List<PostDTO>();
            foreach (var item in Posts) {
            list.Add(Convert(item));
            }
            return list;
        }

        public static List<UserDTO> Convert(List<UserInfo> u) {
           var l = new List<UserDTO>();
            foreach (var item in u) { 
            var user =HomeController.Convert(item);
                l.Add(user);
            }
            return l;
        }
    }

}