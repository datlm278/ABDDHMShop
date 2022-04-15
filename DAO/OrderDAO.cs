using BTL_LTWeb.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_LTWeb.DAO
{
    public class OrderDAO
    {
        public IEnumerable<Order> ListAll(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = DbContext.GetDbContext().Orders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Product.name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.Product.name).ToPagedList(page, pageSize);
        }

        public Order ViewDetails(int id)
        {
            return DbContext.GetDbContext().Orders.Find(id);
        }

        public bool Update(Order entity)
        {
            try
            {
                var order = DbContext.GetDbContext().Orders.Find(entity.id);
                order.quantity = entity.quantity;
                order.data = entity.data;
                order.amount = entity.amount;
                order.status = entity.status;

                DbContext.GetDbContext().SaveChanges();
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
                var order = DbContext.GetDbContext().Orders.Find(id);
                DbContext.GetDbContext().Orders.Remove(order);
                DbContext.GetDbContext().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}