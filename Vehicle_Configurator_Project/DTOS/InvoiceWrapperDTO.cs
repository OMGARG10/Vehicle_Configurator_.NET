namespace Vehicle_Configurator_Project.DTOS
{
    public class InvoiceWrapperDTO
    {
        public InvoiceHeaderDTO InvoiceHeader { get; set; }
        public List<InvoiceDetailDTO> InvoiceDetails { get; set; }
        public int ModelId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
    }
}
