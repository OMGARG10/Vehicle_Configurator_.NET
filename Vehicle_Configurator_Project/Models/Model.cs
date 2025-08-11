using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vehicle_Configurator_Project.Models
{
    [Table("model_master")]
    public class Model
    {
        [Key]
        [Column("model_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModelId { get; set; }

        [Column("model_name")]
        public string ModelName { get; set; }

        [ForeignKey(nameof(Manufacturer))]
        [Column("mfg_id")]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        [ForeignKey(nameof(Segment))]
        [Column("seg_id")]
        public int SegmentId { get; set; }
        public Segment Segment { get; set; }

        public List<VehicleDetail> DefaultComponents { get; set; }

        [Column("min_qty")]
        public int MinQty { get; set; }

        public decimal Price { get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; }
    }
}
