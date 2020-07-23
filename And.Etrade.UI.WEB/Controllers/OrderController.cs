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
    public class OrderController : AndControllerBase
    {
        // GET: Order
        [Route("Giveorder")]
        public ActionResult AddressList()
        {
            var db = new AndDB();
            var data = db.Addresses.Where(x => x.UserID == LoginUserID).ToList();
            return View(data);
        }

        public ActionResult CreateUserAddress(UserAddress entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.CreateUserID = LoginUserID;
            entity.IsActive = true;
            entity.UserID = LoginUserID;

            var db = new AndDB();
            db.Addresses.Add(entity);
            db.SaveChanges();
            return RedirectToAction("AddressList");
        }
    }
}