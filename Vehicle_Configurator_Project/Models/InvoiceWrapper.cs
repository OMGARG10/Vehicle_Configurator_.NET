using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Vehicle_Configurator_Project.Models
{
    public class InvoiceWrapper
    {
        public int UserId { get; set; }
        public int ModelId { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public InvoiceHeader? InvoiceHeader { get; set; }  // Nullable

        [JsonIgnore]
        public List<InvoiceDetail>? InvoiceDetails { get; set; }  // Nullable

        public List<InvoiceComponentDetail> Details { get; set; }

        public class InvoiceComponentDetail
        {
            public int CompId { get; set; }
            public string IsAlternate { get; set; } // "Y" or "N"
            public int? SelectedAltCompId { get; set; }
        }
    }
}
