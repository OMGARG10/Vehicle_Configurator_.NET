using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IRepositories
{
    public interface IInvoiceDetailRepository
    {
        Task<List<InvoiceDetail>> GetAllAsync();
        Task<InvoiceDetail> AddAsync(InvoiceDetail detail);
        Task<List<InvoiceDetail>> GetByInvoiceIdAsync(int invoiceId);
        Task AddRangeAsync(IEnumerable<InvoiceDetail> invoiceDetails);
    }
}
