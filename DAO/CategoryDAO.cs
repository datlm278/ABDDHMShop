using BTL_LTWeb.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_LTWeb.DAO
{
    public class CategoryDAO
    {
        private WebBanHangDbContext dbContext = null;

        public CategoryDAO ()
        {
            dbContext = new WebBanHangDbContext ();
        }

        public IEnumerable<Catalog> ListAll (string searchString, int page, int pageSize)
        {
            IQueryable<Catalog> model = dbContext.Catalogs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x=>x.name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.name).ToPagedList(page, pageSize);
        }

        public List<Catalog> GetAllCategory ()
        {
            return dbContext.Catalogs.ToList();
        }

        public Catalog GetByName(string name)
        {
            return dbContext.Catalogs.SingleOrDefault(x => x.name == name);
        }

        public Catalog GetById (int id)
        {
            return dbContext.Catalogs.Find(id);
        }

        public int Create(Catalog category)
        {
            dbContext.Catalogs.Add(category);
            dbContext.SaveChanges();
            return category.id;
        }

        public bool Update (Catalog entity)
        {
            try
            {
                var category = dbContext.Catalogs.Find(entity.id);
                category.name = entity.name;
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete (int id)
        {
            try
            {
                var category = dbContext.Catalogs.Find(id);
                dbContext.Catalogs.Remove(category);
                dbContext.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}