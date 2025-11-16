using System.ComponentModel.DataAnnotations.Schema;

namespace GAB.Models
{
    [Table("locations")]                     // maps to table `locations`
    public class Location
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("address")]
        public string Address { get; set; } = string.Empty;

        [Column("hours")]
        public string? Hours { get; set; }

        [Column("maps")]
        public string? Maps { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
