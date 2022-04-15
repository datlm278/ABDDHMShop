namespace BTL_LTWeb.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [StringLength(50)]
        [DisplayName("Name")]

        public string name { get; set; }

        [DisplayName("Price")]
        [Required(ErrorMessage = "Price cannot be empty")]
        [Column(TypeName = "money")]
        public decimal price
        {
            get; set;
        }

        [StringLength(50)]
        [DisplayName("Content")]
        public string content { get; set; }

        [DisplayName("Discount")]
        public int? discount { get; set; }

        [Required(ErrorMessage = "Image cannot be empty")]
        [DisplayName("Image")]
        [StringLength(50)]
        public string image_link { get; set; }

        [DisplayName("Create at")]
        public DateTime? time_created { get; set; }

        [DisplayName("Views")]
        public int views { get; set; }

        [Required(ErrorMessage = "Category has not been selected")]
        [DisplayName("Category")]
        public int catalog_id { get; set; }

        public virtual Catalog Catalog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
