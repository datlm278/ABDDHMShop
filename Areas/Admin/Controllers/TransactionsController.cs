using BTL_LTWeb.DAO;
using BTL_LTWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    public class TransactionsController : BaseController
    {
        // GET: Admin/Transactions
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var adminDAO = new TransactionDAO();
            var model = adminDAO.ListAll(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var transaction = new TransactionDAO().ViewDetails(id);
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction transaction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var transactionDAO = new TransactionDAO();
                    var result = transactionDAO.Update(transaction);
                    if (result)
                    {
                        SetAlert("Update transaction successfully", "success");

                        return RedirectToAction("Index", "Transactions");
                    }
                    else
                    {
                        SetAlert("Update transaction failed", "error");
                    }

                }
                return View(transaction);
            }
            catch
            {
                SetAlert("Update transaction failed", "error");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var transaction = new TransactionDAO().ViewDetails(id);
            if (transaction == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(transaction);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                var transactionDAO = new TransactionDAO();
                var transaction = transactionDAO.ViewDetails(id);
                if (transaction == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                var res = transactionDAO.Delete(id);
                if (res)
                {
                    SetAlert("Delete transaction successfully", "success");
                    return RedirectToAction("Index", "Transactions");
                }
                else
                {
                    SetAlert("Delete transaction failed", "error");
                }
                return View(transaction);
            }
            catch
            {
                SetAlert("Delete transaction failed", "error");
                return View();
            }
        }
    }
}