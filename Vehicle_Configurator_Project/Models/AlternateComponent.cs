using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Vehicle_Configurator_Project.Models
{
    [Table("alternate_component_master")]
    public class AlternateComponent
    {
        [Key]
        [Column("alt_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AltId { get; set; }

        [ForeignKey(nameof(Model))]
        [Column("model_id")]
        [JsonIgnore]
        public int ModelId { get; set; }
        public Model Model { get; set; }

        [ForeignKey(nameof(BaseComponent))]
        [Column("comp_id")]
        [JsonIgnore]
        public int BaseComponentId { get; set; }
        public Component BaseComponent { get; set; }

        [ForeignKey(nameof(AlternateComponentEntity))]
        [Column("alt_comp_id")]
        public int AlternateComponentId { get; set; }
        public Component AlternateComponentEntity { get; set; }

        [Column("delta_price")]
        public decimal? DeltaPrice { get; set; }
    }
}
