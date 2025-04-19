using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.DomainModels
{
    [Table("suppliers")]
    public class Supplier
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("contact_name")]
        public string? ContactName { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("email")]
        public string? Email { get; set; }

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

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
