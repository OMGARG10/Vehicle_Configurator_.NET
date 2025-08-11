using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Vehicle_Configurator_Project.Models
{
    [Table("seg_mfg_association")]
    [Index(nameof(SegId), nameof(MfgId), IsUnique = true)]
    public class SegmentManufacturerAssociation
    {
        [Key]
        [Column("seg_mfg_assoc_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SegMfgAssocId { get; set; }

        [ForeignKey(nameof(Segment))]
        [Column("seg_id")]
        public int SegId { get; set; }
        public Segment Segment { get; set; }

        [ForeignKey(nameof(Manufacturer))]
        [Column("mfg_id")]
        public int MfgId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
