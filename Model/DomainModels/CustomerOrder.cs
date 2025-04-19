using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.DomainModels
{
    [Table("customer_orders")]
    public class CustomerOrder
    {
        [Key]
        [Column("id")]
        public Guid ID { get; set; }

        [ForeignKey("Customer")]
        [Column("customer_id")]
        public int CustomerID { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("gst")]
        public decimal Gst { get; set; }

        [Column("total_amount")]
        public decimal TotalAmount { get; set; }

        [Column("status")]
        public OrderStatus Status { get; set; }

        [ForeignKey("User")]
        [Column("operator_id")]
        public int UserID { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public virtual Customer? Customer { get; set; }

        public virtual User? User { get; set; } 
    }

    public enum OrderStatus
    {
        Pending,       
        Processing,   
        Completed,     
        Cancelled      
    }
}

