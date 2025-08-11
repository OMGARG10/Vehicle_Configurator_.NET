using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Vehicle_Configurator_Project.Models
{
    [Table("segment_master")]
    public class Segment
    {
        [Key]
        [Column("seg_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SegId { get; set; }

        [Required]
        [Column("seg_name")]
        public string SegName { get; set; }

        [Required]
        [Column("min_quantity")]
        public int MinQuantity { get; set; }

        // Navigation property (One-to-Many with Model)
        [JsonIgnore] // Equivalent to @JsonBackReference in Java
        public virtual ICollection<Model> Models { get; set; }
    }
}
