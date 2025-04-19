using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.DomainModels
{
    [Table("supplier_orders")]
    public class SupplierOrder
    {
        [Key]
        [Column("id")]
        public Guid ID { get; set; }

        [ForeignKey("Supplier")]
        [Column("supplier_id")]
        public int SupplierID { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; }

        [Column("gst")]
        public decimal Gst { get; set; }

        [Column("total_amount")]
        public decimal TotalAmount { get; set; }

        [Column("status")]
        public SupplierOrderStatus Status { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public virtual Supplier? Supplier { get; set; }
    }

    public enum SupplierOrderStatus
    {
        Pending,
        Submitted,
        Cancelled,
        Completed
    }
}
