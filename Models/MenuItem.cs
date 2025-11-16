using System.ComponentModel.DataAnnotations.Schema;

namespace GAB.Models
{
    [Table("menu_items")]               // 👈 map table name
    public class MenuItem
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("price", TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }

        [Column("image")]
        public string? Image { get; set; }

        [Column("category")]
        public string Category { get; set; } = "Sandwiches";

        // Pomelo maps TINYINT(1) <-> bool automatically
        [Column("is_vegetarian")]
        public bool IsVegetarian { get; set; }

        [Column("is_spicy")]
        public bool IsSpicy { get; set; }

        [Column("is_signature")]
        public bool IsSignature { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
