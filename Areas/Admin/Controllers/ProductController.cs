using BTL_LTWeb.DAO;
using BTL_LTWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var productDAO = new ProductDAO();
            var model = productDAO.ListAll(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetCategory();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var productDAO = new ProductDAO();
                    int res = productDAO.Create(product);
                    if (res > 0)
                    {
                        SetAlert("Add Product successfully", "success");
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        SetAlert("Add Category failed", "error");
                        ModelState.AddModelError("", "Add Category failed");
                    }
                }
                SetCategory();
                return View(product);
            }
            catch
            {
                SetAlert("Add product failed", "error");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SetCategory();
            var category = new ProductDAO().ViewDetails(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var productDAO = new ProductDAO();
                    var res = productDAO.Update(product);
                    if (res)
                    {
                        SetAlert("Update product successfully", "success");
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        SetAlert("Update product failed", "error");
                    }
                }
                return View(product);
            } 
            catch
            {
                SetAlert("Update product failed", "error");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = new ProductDAO().ViewDetails(id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                var productDAO = new ProductDAO();
                var product = productDAO.ViewDetails(id);
                if (product == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                var res = productDAO.Delete(id);
                if (res)
                {
                    SetAlert("Delete product successfully", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    SetAlert("Update product failed", "error");
                }
                return View(product);
            }
            catch
            {
                SetAlert("Update product failed", "error");
                return View();
            }
        }

        public void SetCategory(int? selectedId = null)
        {
            var categoryDAO = new CategoryDAO();
            ViewBag.catalog_id = new SelectList(categoryDAO.GetAllCategory(), "id", "name", selectedId);
        }
    }
}