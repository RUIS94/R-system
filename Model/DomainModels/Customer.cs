using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.DomainModels
{
    [Table("customers")]
    public class Customer
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }

        [Column("username")]
        public string? UserName { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("vip_level")]
        public string? VIPlevel { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }
    }

    //public enum VIPlevel
    //{ 
    //    Regular,
    //    Silver,
    //    Gold,
    //    Platinum
    //}
}
