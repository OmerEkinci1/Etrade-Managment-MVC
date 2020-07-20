using And.Etrade.Core.Model;
using And.Etrade.Core.Model.Entity;
using And.Etrade.UI.WEB.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.Etrade.UI.WEB.Controllers
{
    public class HomeController : AndControllerBase
    {
        AndDB db = new AndDB();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.isLogin = this.isLogin;
            var data = db.Products.OrderByDescending(x => x.CreateDate).Take(5).ToList();
            return View(data);
        }

        public PartialViewResult GetMenu()
        {         
            var menus = db.Categories.Where(x => x.ParentID == 0).ToList();
            return PartialView(menus);
        }
        [Route("Member-Login")]
        public ActionResult Login()
        {          
            return View();
        }
        [HttpPost]
        [Route("Member-Login")]
        public ActionResult Login(string Email,string Password)
        {
            var users = db.Users.Where(x => x.Email == Email && x.Password == Password && x.IsActive == true && x.IsAdmin == false).ToList();
            if (users.Count==1)
            {
                Session["LoginUserID"] = users.FirstOrDefault().ID;
                Session["LoginUser"] = users.FirstOrDefault();
                return Redirect("/");
            }
            else
            {
                ViewBag.Error = "Wrong User or Password";
                return View();
            }   
        }
        [Route("Member-Register")]
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        [Route("Member-Register")]
        public ActionResult CreateUser(User entity)
        {
            try
            {
                entity.CreateDate = DateTime.Now;
                entity.CreateUserID = 1;
                entity.IsActive = true;
                entity.IsAdmin = false;

                db.Users.Add(entity);
                db.SaveChanges();
                return Redirect("/");
            }
            catch (Exception ex)
            {
                return View();
            }
            

            
        }
    }
}