using BTL_LTWeb.DAO;
using BTL_LTWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var userDAO = new UserDAO();
            var model = userDAO.ListAll(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userDAO = new UserDAO();
                    var encrypt = Encryptor.MD5Hash(user.password);
                    user.password = encrypt;
                    int res = userDAO.Create(user);
                    if (res > 0)
                    {
                        SetAlert("Add User successfully", "success");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        SetAlert("Add User failed", "error");

                        ModelState.AddModelError("", "Add user failed");
                    }
                }
                return View(user);
            }
            catch
            {
                SetAlert("Add User failed", "error");

                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new UserDAO().GetById(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userDAO = new UserDAO();

                    
                    var res = userDAO.Update(user);

                    if (res)
                    {
                        SetAlert("Update User successfully", "success");

                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        SetAlert("Update User failed", "error");

                        ModelState.AddModelError("", "Update User failed");
                    }
                }
                return View(user);
            }
            catch
            {
                SetAlert("Update User failed", "error");

                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var user = new UserDAO().GetById(id);
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                var userDAO = new UserDAO();
                var user = userDAO.GetById(id);
                if (user == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                var res = userDAO.Delete(id);
                if (res)
                {
                    SetAlert("Delete product successfully", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Update product failed", "error");
                }
                return View(user);
            }
            catch
            {
                SetAlert("Update product failed", "error");
                return View();
            }
        }
    }
}