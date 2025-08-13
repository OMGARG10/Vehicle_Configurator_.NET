using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Vehicle_Configurator_Project.Models
{
    [Table("invoice_header")]
    public class InvoiceHeader
    {
        [Key]
        [Column("inv_id")]
        public int InvId { get; set; }

        // Many-to-One relationship with User
        [Required]
        [ForeignKey("UserId")]
        [JsonIgnore]
        public User User { get; set; } = null!;
        [Column("userid")]
        public int UserId { get; set; }

        [Required]
        [Column("inv_date", TypeName = "datetime")]
        public DateTime InvDate { get; set; } = DateTime.Now;

        // Many-to-One with Model (lazy loading can be configured in DbContext)
        [Required]
        [ForeignKey("ModelId")]
        [JsonIgnore]
        public Model Model { get; set; } = null!;
        [Column("model_id")]
        public int ModelId { get; set; }

        // One-to-Many relationship with InvoiceDetail
        [JsonIgnore]
        public List<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

        [Required]
        [Column("quantity")]
        public int Quantity { get; set; }

        [Required]
        [Column("total_amount", TypeName = "decimal(12,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [Column("tax", TypeName = "decimal(12,2)")]
        public decimal Tax { get; set; }

        [Required]
        [Column("final_amount", TypeName = "decimal(12,2)")]
        public decimal FinalAmount { get; set; }

        [Required]
        [Column("cust_details", TypeName = "nvarchar(500)")]
        public string CustDetails { get; set; } = null!;
    }
}
