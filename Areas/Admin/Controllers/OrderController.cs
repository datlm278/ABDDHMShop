using BTL_LTWeb.DAO;
using BTL_LTWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var orderDAO = new OrderDAO();
            var model = orderDAO.ListAll(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var order = new OrderDAO().ViewDetails(id);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var orderDAO = new OrderDAO();
                    var result = orderDAO.Update(order);
                    if (result)
                    {
                        SetAlert("Update order successfully", "success");

                        return RedirectToAction("Index", "Order");
                    }
                    else
                    {
                        SetAlert("Update order failed", "error");
                    }

                }
                return View(order);
            }
            catch
            {
                SetAlert("Update order failed", "error");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var order = new OrderDAO().ViewDetails(id);
            if (order == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                var orderDAO = new OrderDAO();
                var order = orderDAO.ViewDetails(id);
                if (order == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                var res = orderDAO.Delete(id);
                if (res)
                {
                    SetAlert("Delete order successfully", "success");
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    SetAlert("Delete order failed", "error");
                }
                return View(order);
            }
            catch
            {
                SetAlert("Delete order failed", "error");
                return View();
            }
        }
    }
}