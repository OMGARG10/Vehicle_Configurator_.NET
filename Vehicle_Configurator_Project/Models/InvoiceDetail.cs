using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Vehicle_Configurator_Project.Models
{
    [Table("invoice_detail")]
    public class InvoiceDetail
    {
        [Key]
        [Column("inv_dtl_id")]
        public int InvDtlId { get; set; }

        // Many-to-One relationship with InvoiceHeader
        [Required]
        [ForeignKey("InvoiceHeaderId")]
        [JsonIgnore]
        public InvoiceHeader InvoiceHeader { get; set; } = null!;
        [Column("inv_id")]
        public int InvoiceHeaderId { get; set; }

        // Many-to-One relationship with Component
        [Required]
        [ForeignKey("ComponentId")]
        public Component Component { get; set; } = null!;
        [Column("comp_id")]
        public int ComponentId { get; set; }

        [Required]
        [Column("is_alternate", TypeName = "char(1)")]
        public string IsAlternate { get; set; } = "N";  // default "N" means not alternate
    }
}
