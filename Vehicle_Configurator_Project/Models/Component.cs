using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vehicle_Configurator_Project.Models
{
    [Table("component_master")]
    public class Component
    {
        [Key]
        [Column("comp_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompId { get; set; }

        [Column("comp_name")]
        public string CompName { get; set; }

        public Component() { }
    }
}
