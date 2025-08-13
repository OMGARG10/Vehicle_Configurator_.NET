namespace Vehicle_Configurator_Project.DTOS
{
    public class InvoiceHeaderDTO
    {
        public int? InvId { get; set; }
        public string CustDetails { get; set; }
        public string InvDate { get; set; }  // Use string if your Java microservice expects date as string
        public int? Quantity { get; set; }
        public double? FinalAmount { get; set; }
        public double? Tax { get; set; }
        public double? TotalAmount { get; set; }
    }
}
