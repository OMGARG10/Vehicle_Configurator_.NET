using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Vehicle_Configurator_Project.Models
{
    [Table("vehicle_detail")]
    public class VehicleDetail
    {
        [Key]
        [Column("config_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConfigId { get; set; }

        [ForeignKey(nameof(Model))]
        [Column("model_id")]
        [JsonIgnore] // Prevent cyclic reference when serializing
        public int ModelId { get; set; }

        [JsonIgnore]
        public Model Model { get; set; }

        [ForeignKey(nameof(Component))]
        [Column("comp_id")]
        public int? ComponentId { get; set; }
        public Component Component { get; set; }

        [Column("comp_type")]
        [Required]
        public char CompType { get; set; }

        [Column("is_configurable")]
        [Required]
        public char IsConfigurable { get; set; }
    }
}
