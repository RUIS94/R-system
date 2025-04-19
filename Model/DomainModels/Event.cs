using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.DomainModels
{
    [Table("events")]
    public class Event
    {
        [Key]
        [Column("id")]
        public int ID { get; set; } 

        [Column("summary")]
        public string? Summary { get; set; }

        [Column("event_date")]
        public DateTime EventDate { get; set; } 

        [Column("notes")]
        public string? Notes { get; set; } 
    }
}
