using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.DomainModels
{
    [Table("product_suppliers")]
    public class ProductSupplier
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }

        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductID { get; set; }

        [ForeignKey("Supplier")]
        [Column("supplier_id")]
        public int SupplierID { get; set; }

        [Column("cost")]
        public decimal Cost { get; set; }

        [Column("gst")]
        public decimal Gst { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public virtual Product? Product { get; set; }

        public virtual Supplier? Supplier { get; set; }
    }
}
