using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.DomainModels
{
    [Table("help_docs")]
    public class HelpDoc
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }

        [Column("name")]
        public string? Name { get; set; } 

        [Column("type")]
        public string? Type { get; set; }

        [Column("link")]
        public string? Link { get; set; }
    }
}
