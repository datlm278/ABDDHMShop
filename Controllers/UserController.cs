using BTL_LTWeb.Areas.Admin.Data;
using BTL_LTWeb.DAO;
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
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var userDAO = new UserDAO();
                var res = userDAO.Login(model.userName, Encryptor.MD5Hash(model.password));
                if (res)
                {
                    var user = userDAO.GetByUserName(model.userName);
                    var userSession = new UserSession();
                    userSession.UserName = user.username;
                    userSession.UserID = user.id;
                    userSession.Email = user.email;
                    userSession.Name = user.name;
                    Session.Add(Constant.USER_SESSION, userSession);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login unsuccessful! Username or password is incorrect");
                }
            }
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session[Constant.ADMIN_SESSION] = null;
            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var userDAO = new UserDAO();
                if (userDAO.CheckUserName(model.username))
                {
                    ModelState.AddModelError("", "username already exists");
                }
                else if (userDAO.CheckEmail(model.email))
                {
                    ModelState.AddModelError("", "email already exists");
                }
                else
                {
                    var user = new User();
                    user.name = model.name;
                    user.email = model.email;
                    user.password = Encryptor.MD5Hash(model.password);
                    user.username = model.username;
                    user.address = model.address;
                    user.time_created = DateTime.Now;
                    var result = userDAO.Create(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Successful registration";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "registration failed");
                    }
                }
            }
            return View(model);
        }
    }
}