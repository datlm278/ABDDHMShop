using BTL_LTWeb.DAO;
using BTL_LTWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_LTWeb.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        [ChildActionOnly]
        public PartialViewResult CategoryPartial()
        {
            CategoryDAO categoryDAO = new CategoryDAO();
            var model = categoryDAO.GetAllCategory();
            return PartialView(model);
        }

        public ViewResult FilterProductByCategory(int id)
        {
            CategoryDAO categoryDAO = new CategoryDAO();
            var category = categoryDAO.GetById(id);
            if (category == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            ProductDAO productDAO = new ProductDAO();
            List<Product> products = productDAO.GetByCategory(id);
            if (products.Count == 0)
            {
                ViewBag.products = "There are no products in this category";
            }
            return View(products);
        }
        
        public ViewResult ProductDetails(int id)
        {
            Product product = DbContext.GetDbContext().Products.SingleOrDefault(x => x.id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(product);
        }
        
        [ChildActionOnly]
        public PartialViewResult ProductDetailsPartial(int id)
        {
            ProductDAO productDAO = new ProductDAO();
            var product = productDAO.GetByCategory(id);
            return PartialView (product);
        }
    }
}