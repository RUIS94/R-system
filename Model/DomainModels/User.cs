using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.DomainModels
{
    [Table("users")]
    public class User
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

        [ForeignKey("Role")]
        [Column("role_id")]
        [JsonIgnore]
        public int RoleID { get; set; }
        [JsonIgnore]
        public virtual Role? Role { get; set; }

        [NotMapped]
        public string? RoleName { get; set; }
    }
}
