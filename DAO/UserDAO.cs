using BTL_LTWeb.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_LTWeb.DAO
{
    public class UserDAO
    {
        private WebBanHangDbContext dbContext = null;

        public UserDAO()
        {
            dbContext = new WebBanHangDbContext();
        }

        public IEnumerable<User> ListAll(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = dbContext.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.name.Contains(searchString) || x.username.Contains(searchString));
            }
            return model.OrderByDescending(x => x.name).ToPagedList(page, pageSize);
        }

        public User GetByUserName(string name)
        {
            return DbContext.GetDbContext().Users.SingleOrDefault(x => x.username == name);
        }

        public User GetById(int id)
        {
            return dbContext.Users.Find(id);
        }

        public int Create(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return user.id;
        }

        public bool Update(User entity)
        {
            try
            {
                var user = DbContext.GetDbContext().Users.Find(entity.id);
                user.name = entity.name;
                user.email = entity.email;
                user.address = entity.address;
                user.time_created = entity.time_created;
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
                var user = dbContext.Users.Find(id);
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Login(string userName, string password)
        {
            var result = dbContext.Users.Count(x => x.username == userName && x.password == password);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckUserName (string userName)
        {
            return DbContext.GetDbContext().Users.Count(x => x.username == userName) > 0;
        }

        public bool CheckEmail (string email)
        {
            return DbContext.GetDbContext().Users.Count(x=>x.email == email) > 0;
        }
    }
}