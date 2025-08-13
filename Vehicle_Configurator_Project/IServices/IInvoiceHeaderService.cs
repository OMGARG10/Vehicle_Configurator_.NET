using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IServices
{
    public interface IInvoiceHeaderService
    {
        Task<InvoiceHeader> CreateInvoiceAsync(int userId, InvoiceCreateRequest request);
        Task<InvoiceHeader?> GetInvoiceWithDetailsAsync(int invoiceId);
    }
}
