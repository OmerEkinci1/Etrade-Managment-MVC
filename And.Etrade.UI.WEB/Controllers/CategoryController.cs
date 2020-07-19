using And.Etrade.Core.Model;
using And.Etrade.UI.WEB.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.Etrade.UI.WEB.Controllers
{
    public class CategoryController : AndControllerBase
    {
        // GET: Category
        [Route("Category/{Name}/{ID}")]
        public ActionResult Index(string Name,int ID)
        {
            var db = new AndDB();
            var data = db.Products.Where(x => x.IsActive == 1 && x.CategoryID == ID).ToList();
            ViewBag.category = db.Categories.Where(x => x.ID == ID).FirstOrDefault();
            return View(data);
        }
    }
}