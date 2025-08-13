namespace Vehicle_Configurator_Project.Models
{
    public class InvoiceCreateRequest
    {
        public int UserId { get; set; }
        public int ModelId { get; set; }
        public int Quantity { get; set; }
        public List<ComponentSelection> Details { get; set; } = new();
    }
}
