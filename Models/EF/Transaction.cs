namespace BTL_LTWeb.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transaction()
        {
            Orders = new HashSet<Order>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(10)]
        public string status { get; set; }

        [Required]
        [StringLength(50)]
        public string user_name { get; set; }

        [Required]
        [StringLength(50)]
        public string user_email { get; set; }

        public int amount { get; set; }

        [StringLength(30)]
        public string payment { get; set; }

        [StringLength(50)]
        public string payment_info { get; set; }

        [StringLength(200)]
        public string message { get; set; }

        public int? security { get; set; }

        [Column(TypeName = "date")]
        public DateTime? time_created { get; set; }

        public int? user_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        public virtual User User { get; set; }
    }
}
