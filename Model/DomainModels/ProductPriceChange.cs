using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.DomainModels
{
    [Table("product_price_changes")]
    public class ProductPriceChange
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }

        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductID { get; set; }

        [Column("change_date")]
        public DateTime ChangeDate { get; set; }

        [Column("previous_price")]
        public decimal PreviousPrice { get; set; }

        [Column("new_price")]
        public decimal NewPrice { get; set; }

        public virtual Product? Product { get; set; }
    }
}
