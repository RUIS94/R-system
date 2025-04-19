using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.DomainModels
{
    [Table("product_cost_changes")]
    public class ProductCostChange
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }

        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductID { get; set; } 

        [Column("change_date")]
        public DateTime ChangeDate { get; set; }  

        [Column("previous_cost")]
        public decimal PreviousCost { get; set; } 

        [Column("new_cost")]
        public decimal NewCost { get; set; }

        public virtual Product? Product { get; set; }
    }
}
