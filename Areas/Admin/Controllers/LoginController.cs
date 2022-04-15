using BTL_LTWeb.Areas.Admin.Data;
using BTL_LTWeb.DAO;
using System;
using System.Web.Mvc;
using System.Web.Security;
using BTL_LTWeb.Areas.Admin.Models;
using BTL_LTWeb.Models.EF;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var adminDAO = new AdminDAO();
                if (adminDAO.CheckUserName(model.username))
                {
                    ModelState.AddModelError("", "username already exists");
                }
                else if (adminDAO.CheckName(model.name))
                {
                    ModelState.AddModelError("", "admin already exists");
                }
                else
                {
                    var admin = new Administrator();
                    admin.name = model.name;
                    admin.password = Encryptor.MD5Hash(model.password);
                    admin.username = model.username;
                    var result = adminDAO.Register(admin);
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

        [HttpGet]
        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login (LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var adminDAO = new AdminDAO();
                var res = adminDAO.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (res)
                {
                    var admin = adminDAO.GetByUserName(model.UserName);
                    var adminSession = new AdminSession();
                    adminSession.UserName = admin.username;
                    adminSession.UserId = admin.id;
                    adminSession.Name = admin.name;
                    Session.Add(Constant.ADMIN_SESSION, adminSession);

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
            Session[Constant.USER_SESSION] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}