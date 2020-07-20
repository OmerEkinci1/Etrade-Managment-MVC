using And.Etrade.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.Etrade.UI.WEB.Controllers.Base
{
    public class BasketController : AndControllerBase
    {
        // GET: BASKET
        [HttpPost]
        public JsonResult AddProduct(int productID,int quantity)
        {
            var db = new AndDB();
            db.Baskets.Add(new Core.Model.Entity.Basket
            {
                CreateDate = DateTime.Now,
                CreateUserID = LoginUserID,
                ProductID = productID,
                Quantity = quantity,
                UserID = LoginUserID
            }) ;
            var dt = db.SaveChanges();
            return Json(dt , JsonRequestBehavior.AllowGet);
        }

        [Route("My-Basket")]
        public ActionResult Index()
        {
            var db = new AndDB();
            var data = db.Baskets.Include("Product").Where(x => x.UserID == LoginUserID).ToList();
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var db = new AndDB();
            var deleteitem = db.Baskets.Where(x => x.ID == id).FirstOrDefault();
            db.Baskets.Remove(deleteitem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}