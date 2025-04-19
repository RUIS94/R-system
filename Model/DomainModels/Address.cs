using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.DomainModels
{
    [Table("addresses")]
    public class Address
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }

        [ForeignKey("Customer")]
        [Column("customer_id")]
        public int CustomerID { get; set; }

        [Column("address_type")]
        public AddressType AddressType { get; set; }

        [Column("street_address")]
        public string? StreetAddress { get; set; }

        [Column("city")]
        public string? City { get; set; }

        [Column("state")]
        public string? State { get; set; }

        [Column("country")]
        public string? Country { get; set; }

        [Column("zip_code")]
        public string? ZipCode { get; set; }

        public virtual Customer? Customer { get; set; }
    }
    public enum AddressType
    {
        Billing,
        Shipping
    }
}
