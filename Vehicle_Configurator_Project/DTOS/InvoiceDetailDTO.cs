namespace Vehicle_Configurator_Project.DTOS
{
    public class InvoiceDetailDTO
    {
        public ComponentDTO Component { get; set; }
        public string IsAlternate { get; set; }  // "Y" or "N"
    }
}
