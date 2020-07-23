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

        public ActionResult CreateOrder(int id)
        {
            var db = new AndDB();
            var basket = db.Baskets.Include("Product").Where(x => x.UserID == LoginUserID).ToList();
            Order order = new Order();
            order.CreateDate = DateTime.Now;
            order.CreateUserID = LoginUserID;
            order.StatusID = 1;
            order.TotalProductPrice = basket.Sum(x => x.Product.Price);
            order.TotalTaxPrice = basket.Sum(x => x.Product.Tax);
            order.TotalDiscount = basket.Sum(x => x.Product.Discount);
            order.TotalPrice = order.TotalProductPrice + order.TotalTaxPrice;
            order.UserAddressID = id;
            order.UserID = LoginUserID;
            order.OrderProducts = new List<OrderProduct>();

            foreach (var item in basket)
            {
                order.OrderProducts.Add(new OrderProduct
                {
                    CreateDate = DateTime.Now,
                    CreateUserID = LoginUserID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity
                });
                db.Baskets.Remove(item);
            }
            db.Orders.Add(order);
            db.SaveChanges();
            var orderid = db.Orders.Where(x => x.UserID == LoginUserID).LastOrDefault().ID;
            return RedirectToAction("Detail", new {id= orderid});
        }

        public ActionResult Detail(int id) 
        {
            var db = new AndDB();
            var data = db.Orders.Include("OrderProducts").Include("OrderProducts.Product")
                .Include("OrderPayments").Include("Status").Include("UserAddress").Where(x => x.ID == id).FirstOrDefault();
            return View(data);      
        }
        
        [Route("MyOrder")]
        public ActionResult Index()
        {
            var db = new AndDB();
            var data = db.Orders.Include("Status").Where(x => x.UserID == LoginUserID).ToList();
            return View(data);
        }
    }
}