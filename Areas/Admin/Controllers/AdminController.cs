using BTL_LTWeb.DAO;
using BTL_LTWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin/Admin
        public ActionResult Index(string searchString, int page = 1, int pageSize = 1)
        {
            var adminDAO = new AdminDAO();
            var model = adminDAO.ListAll(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit (int id)
        {
            var admin = new AdminDAO().ViewDetails(id);
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Administrator admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var adminDAO = new AdminDAO();
                    var result = adminDAO.Update(admin);
                    if (result)
                    {
                        SetAlert("Update Administrator successfully", "success");

                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        SetAlert("Update Administrator failed", "error");
                    }

                }
                return View(admin);
            }
            catch
            {
                SetAlert("Update Administrator failed", "error");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var admin = new AdminDAO().ViewDetails(id);
            if (admin == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(admin);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                var adminDAO = new AdminDAO();
                var admin = adminDAO.ViewDetails(id);
                if (admin == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                var res = adminDAO.Delete(id);
                if (res)
                {
                    SetAlert("Delete admin successfully", "success");
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    SetAlert("Delete admin failed", "error");
                }
                return View(admin);
            }
            catch
            {
                SetAlert("Delete admin failed", "error");
                return View();
            }
        }
    }
}