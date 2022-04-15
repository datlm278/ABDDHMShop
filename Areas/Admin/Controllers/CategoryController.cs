using BTL_LTWeb.DAO;
using System;
using BTL_LTWeb.Models.EF;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var categoryDAO = new CategoryDAO();
            var model = categoryDAO.ListAll(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Catalog catalog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var categoryDAO = new CategoryDAO();
                    int res = categoryDAO.Create(catalog);
                    if (res > 0)
                    {
                        SetAlert("Add Category successfully", "success");
                        return RedirectToAction("Index", "Category");
                    }
                    else
                    {
                        SetAlert("Add Category failed", "error");
                    }
                }
                return View(catalog);
            } 
            catch
            {
                SetAlert("Add Category failed", "error");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = new CategoryDAO().GetById(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Catalog catalog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var categoryDAO = new CategoryDAO();
                    var res = categoryDAO.Update(catalog);
                    if (res)
                    {
                        SetAlert("Update Category successfully", "success");
                        return RedirectToAction("Index", "Category");
                    }
                    else
                    {
                        SetAlert("Update Category failed", "error");
                    }
                }
                return View(catalog);
            }
            catch
            {
                SetAlert("Update Category failed", "error");

                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var category = new CategoryDAO().GetById(id);
            if (category == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                var categoryDAO = new CategoryDAO();
                var category = categoryDAO.GetById(id);
                if (category == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                var res = categoryDAO.Delete(id);
                if (res)
                {
                    SetAlert("Delete category successfully", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    SetAlert("Delete category failed", "error");
                }
                return View(category);
            }
            catch
            {
                SetAlert("Delete category failed", "error");
                return View();
            }
        }
    }
}