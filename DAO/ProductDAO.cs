using BTL_LTWeb.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_LTWeb.DAO
{
    public class ProductDAO
    {
        private WebBanHangDbContext dbContext = null;

        public ProductDAO()
        {
            dbContext = new WebBanHangDbContext();
        }

        public IEnumerable<Product> ListAll(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = dbContext.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.name).ToPagedList(page, pageSize);
        }

        public Product GetByUserName(string productName)
        {
            return dbContext.Products.SingleOrDefault(x => x.name == productName);
        }

        public List<Product> GetByCategory (int categoryId)
        {
            return dbContext.Products.Where(x=>x.catalog_id == categoryId).OrderBy(x=>x.name).ToList();
        }

        public List<Product> GetByView()
        {
            var model = dbContext.Products.Where(x => x.views > 100).ToList();
            return model.OrderByDescending(x => x.views).ToList();
        }

        public Product ViewDetails(int id)
        {
            return dbContext.Products.Find(id);
        }

        public int Create(Product product)
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            return product.id;
        }

        public bool Update(Product entity)
        {
            try
            {
                var product = dbContext.Products.Find(entity.id);
                product.name = entity.name;
                product.price = entity.price;
                product.content = entity.content;
                product.discount = entity.discount;
                product.image_link = entity.image_link;
                product.time_created = entity.time_created;
                product.views = entity.views;
                product.catalog_id = entity.catalog_id;
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var product = dbContext.Products.Find(id);
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}