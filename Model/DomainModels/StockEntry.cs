using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.DomainModels
{
    [Table("stock_entries")]
    public class StockEntry
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }

        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductID { get; set; }

        [ForeignKey("SupplierOrder")]
        [Column("supplier_order_id")]
        public Guid SupplierOrderID { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("cost")]
        public decimal Cost { get; set; }

        [Column("gst")]
        public decimal Gst { get; set; }

        [Column("total_amount")]
        public decimal TotalAmount { get; set; }

        [Column("entry_date")]
        public DateTime EntryDate { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public virtual Product? Product { get; set; }

        public virtual SupplierOrder? SupplierOrder { get; set; }
    }
}
