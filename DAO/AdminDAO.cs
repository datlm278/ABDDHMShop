using BTL_LTWeb.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_LTWeb.DAO
{
    public class AdminDAO
    {

        private WebBanHangDbContext dbContext = null;

        public AdminDAO ()
        {
            dbContext = new WebBanHangDbContext ();
        }

        public IEnumerable<Administrator> ListAll(string searchString, int page, int pageSize)
        {
            IQueryable<Administrator> model = dbContext.Administrators;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.username.Contains(searchString) || x.name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.username).ToPagedList(page, pageSize);
        }

        public Administrator GetByUserName(string userName)
        {
            return dbContext.Administrators.SingleOrDefault(x => x.username == userName);
        }

        public Administrator ViewDetails (int id)
        {
            return dbContext.Administrators.Find(id);
        }

        public int Register (Administrator admin)
        {
            dbContext.Administrators.Add(admin);
            dbContext.SaveChanges ();
            return admin.id;
        }

        public bool Update (Administrator entity)
        {
            try
            {
                var admin = dbContext.Administrators.Find(entity.id);
                admin.name = entity.name;
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
                var admin = dbContext.Administrators.Find(id);
                dbContext.Administrators.Remove(admin);
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool Login(string userName, string password)
        {
            var result = dbContext.Administrators.Count(x => x.username == userName && x.password == password);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckUserName(string userName)
        {
            return DbContext.GetDbContext().Administrators.Count(x => x.username == userName) > 0;
        }

        public bool CheckName(string name)
        {
            return DbContext.GetDbContext().Administrators.Count(x => x.name == name) > 0;
        }
    }
}