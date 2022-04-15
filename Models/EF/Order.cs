namespace BTL_LTWeb.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public int id { get; set; }

        public int quantity { get; set; }

        public int amount { get; set; }

        [Required]
        [StringLength(50)]
        public string data { get; set; }

        public int status { get; set; }

        public int transaction_id { get; set; }

        public int product_id { get; set; }

        public virtual Product Product { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}
