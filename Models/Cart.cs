using BTL_LTWeb.DAO;
using BTL_LTWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_LTWeb.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Amount
        {
            get
            {
                return Price * Quantity;
            }
        }

        public Cart (int id)
        {
            Id = id;
            Product product = DbContext.GetDbContext().Products.SingleOrDefault(x => x.id == Id);
            Name = product.name;
            Image = product.image_link;
            Price = (int)product.price;
            Quantity = 1;
        }
    }
}