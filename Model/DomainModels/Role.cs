using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.DomainModels
{
    [Table("roles")]
    public class Role
    {
        [Key]
        [Column("id")]
        public int RoleID { get; set; }

        [Column("role_name")]
        public string? Name { get; set; }

        [Column("role_description")]
        public string? Description { get; set; }
    }
}
