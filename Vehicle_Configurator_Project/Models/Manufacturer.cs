using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Vehicle_Configurator_Project.Models
{
    [Table("mfg_master")]
    public class Manufacturer
    {
        [Key]
        [Column("mfg_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MfgId { get; set; }

        [Required]
        [Column("mfg_name")]
        public string MfgName { get; set; }

        // Navigation property for related Models
        [JsonIgnore] // Equivalent to @JsonBackReference in Java
        public virtual ICollection<Model> Models { get; set; }
    }
}
