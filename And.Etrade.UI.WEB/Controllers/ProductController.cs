using And.Etrade.Core.Model;
using And.Etrade.UI.WEB.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.Etrade.UI.WEB.Controllers
{
    public class ProductController : AndControllerBase
    {
        // GET: Product
        [Route("Product/{title}/{id}")]
        public ActionResult Detail(string title,int id)
        {
            var db = new AndDB();
            var prod = db.Products.Where(x => x.ID == id).FirstOrDefault();
            return View(prod);
        }
    }
}