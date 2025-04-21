using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Model.DomainModels
{
    [Table("accounts")]
    public class Account
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }

        [ForeignKey("Customer")]
        [Column("customer_id")]
        public int CustomerID { get; set; }

        [Column("balance")]
        public decimal Balance { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public virtual Customer? Customer { get; set; }

        [NotMapped]
        public string? UserName { get; set; }
    }
}
