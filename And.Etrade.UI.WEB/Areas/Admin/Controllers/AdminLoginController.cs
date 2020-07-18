using And.Etrade.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.Etrade.UI.WEB.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        AndDB db = new AndDB();
        // GET: Admin/AdminLogin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Email, string Password)
        {
            var data = db.Users.Where(x => x.Email == Email && x.Password == Password && x.IsActive==true && x.IsAdmin==true).ToList();
            if (data.Count == 1)
            {
                //Doğru girişş
                Session["AdminLoginUser"] = data.FirstOrDefault();
                return Redirect("/admin");
            }
            else
            {
                //hatalı giriş
                return View();
            }
            
        }
    }
}