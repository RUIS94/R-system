using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.DomainModels
{
    [Table("product_stock_changes")]
    public class ProductStockChange
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }

        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductID { get; set; }

        [Column("change_date")]
        public DateTime ChangeDate { get; set; }

        [Column("previous_stock")]
        public int PreviousStock { get; set; }

        [Column("new_stock")]
        public int NewStock { get; set; }

        public virtual Product? Product { get; set; }
    }
}
