using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IServices
{
    public interface IInvoiceDetailService
    {
        Task<List<InvoiceDetail>> GetAllInvoiceDetailsAsync();
        Task<InvoiceDetail> CreateInvoiceDetailAsync(InvoiceDetail detail);
        Task<List<InvoiceDetail>> GetInvoiceDetailsByInvoiceIdAsync(int invoiceId);
    }
}
