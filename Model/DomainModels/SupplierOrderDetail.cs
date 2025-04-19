using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.DomainModels
{
    [Table("supplier_order_details")]
    public class SupplierOrderDetail
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }

        [ForeignKey("SupplierOrder")]
        [Column("order_id")]
        public Guid OrderID { get; set; }

        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductID { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("unit_price")]
        public decimal UnitPrice { get; set; }

        [Column("gst")]
        public decimal Gst { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public virtual SupplierOrder? SupplierOrder { get; set; }

        public virtual Product? Product { get; set; }
    }
}
