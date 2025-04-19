using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.DomainModels
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("barcode")]
        public string? Barcode { get; set; }

        [Column("cost")]
        public decimal Cost { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("inventory")]
        public int Inventory { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("category")]
        public string? Category { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("gst")]
        public decimal Gst { get; set; }
    }
}
