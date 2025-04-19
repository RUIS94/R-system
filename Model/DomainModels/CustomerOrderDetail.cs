using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.DomainModels
{
    [Table("customer_order_details")] 
    public class CustomerOrderDetail
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }

        [ForeignKey("Order")]
        [Column("order_id")]
        public Guid OrderID { get; set; }

        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductID { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("unit_price")]
        public decimal UnitPrice { get; set; }

        [Column("discount")]
        public decimal Discount { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("gst")]
        public decimal Gst { get; set; }

        public virtual CustomerOrder? CustomerOrder { get; set; }

        public virtual Product? Product { get; set; }
    }
}
