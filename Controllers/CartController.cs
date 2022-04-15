using BTL_LTWeb.Areas.Admin.Data;
using BTL_LTWeb.Models;
using BTL_LTWeb.Models.EF;
using BTL_LTWeb.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_LTWeb.Controllers
{
    public class CartController : Controller
    {
        WebBanHangDbContext dbContext = new WebBanHangDbContext();

        #region Cart
        public List<Cart> GetCart()
        {
            List<Cart> carts = Session["Cart"] as List<Cart>;
            if (carts == null)
            {
                carts = new List<Cart>();
                Session["Cart"] = carts;
            }
            return carts;
        }

        public int SumQuantity()
        {
            int sum = 0;
            List<Cart> carts = Session["Cart"] as List<Cart>;
            if (carts != null)
            {
                sum = carts.Sum(x => x.Quantity);
            }
            return sum;
        }

        public int Amount()
        {
            int sum = 0;
            List<Cart> carts = Session["Cart"] as List<Cart>;
            if (carts != null)
            {
                sum = carts.Sum(x => x.Amount);
            }
            return sum;
        }
        public ActionResult Cart()
        {
            if (Session["Cart"] == null)
            {
                RedirectToAction("Index", "Product");
            }
            List<Cart> carts = GetCart();
            ViewBag.SumQuantity = SumQuantity();
            ViewBag.SumPrice = Amount();
            return View(carts);
        }
        public ActionResult Create(int id, string url, FormCollection form)
        {
            Product product = dbContext.Products.SingleOrDefault(x => x.id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> carts = GetCart();
            Cart productInCart = carts.Find(x => x.Id == id);
            if (productInCart == null)
            {
                productInCart = new Cart(id);
                carts.Add(productInCart);
                return Redirect(url);
            }
            else
            {
                productInCart.Quantity++;
                return Redirect(url);
            }
        }

        public ActionResult Update()
        {
            if (Session["Cart"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            List<Cart> carts = GetCart();
            ViewBag.SumQuantity = SumQuantity();
            ViewBag.SumPrice = Amount();

            return View(carts);
        }

        public ActionResult UpdateCart(int id, FormCollection form)
        {
            Product product = dbContext.Products.SingleOrDefault(x => x.id == id);
            if (product ==  null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> carts = GetCart();
            Cart productInCart = carts.SingleOrDefault(x => x.Id == id);
            if (productInCart != null)
            {
                productInCart.Quantity = int.Parse(form.Get("quantity").ToString());
            }
            return RedirectToAction("Update");
        }

        public ActionResult Delete(int id)
        {
            Product product = dbContext.Products.SingleOrDefault(x => x.id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> carts = GetCart();
            Cart productInCart = carts.SingleOrDefault(x => x.Id == id);
            if (productInCart != null)
            {
                carts.RemoveAll(x => x.Id == id);
            }
            if (carts.Count == 0)
            {
                RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Update");
        }
        #endregion

        #region Order Product
        public ActionResult OrderProduct() 
        {
            if (Session[Constant.USER_SESSION] == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (Session["Cart"] == null)
            {
                RedirectToAction("Index", "Home");
            }

            var userSession = Session[Constant.USER_SESSION] as UserSession;

            Transaction transaction = new Transaction();
            transaction.user_id = userSession.UserID;
            transaction.user_email = userSession.Email;
            transaction.user_name = userSession.Name;
            transaction.status = "Delivery";
            transaction.payment = "Cash";
            transaction.amount = Amount();
            transaction.time_created = DateTime.Now;
            dbContext.Transactions.Add(transaction);
            dbContext.SaveChanges();

            List<Cart> carts = GetCart();
            foreach (var item in carts)
            {
                Order order = new Order();
                order.transaction_id = transaction.id;
                order.product_id = item.Id;
                order.quantity = item.Quantity;
                order.amount = item.Amount;
                order.data = "successfull";
                dbContext.Orders.Add(order);
            }
            dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        #endregion

    }
}