using BTL_LTWeb.Models.EF;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_LTWeb.DAO
{
    public class TransactionDAO
    {
        public IEnumerable<Transaction> ListAll(string searchString, int page, int pageSize)
        {
            IQueryable<Transaction> model = DbContext.GetDbContext().Transactions;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.user_name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.user_name).ToPagedList(page, pageSize);
        }

        public Transaction ViewDetails(int id)
        {
            return DbContext.GetDbContext().Transactions.Find(id);
        }

        public bool Update(Transaction entity)
        {
            try
            {
                var transaction = DbContext.GetDbContext().Transactions.Find(entity.id);
                transaction.user_name = entity.user_name;
                transaction.user_email = entity.user_email;
                transaction.amount = entity.amount;
                transaction.payment = entity.payment;
                transaction.message = entity.message;
                transaction.status = entity.status;
                transaction.time_created = entity.time_created;
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
                var transaction = DbContext.GetDbContext().Transactions.Find(id);
                DbContext.GetDbContext().Transactions.Remove(transaction);
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