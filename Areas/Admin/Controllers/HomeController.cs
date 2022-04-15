using BTL_LTWeb.Areas.Admin.Data;
using BTL_LTWeb.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var adminDAO = new AdminDAO();
            var model = adminDAO.ListAll(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
    }
}