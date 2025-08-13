using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IRepositories
{
    public interface IInvoiceHeaderRepository
    {
        Task<InvoiceHeader> AddInvoiceAsync(InvoiceHeader invoice);
        Task AddAsync(InvoiceHeader invoice);
        Task SaveChangesAsync();
        Task<InvoiceHeader?> GetByIdAsync(int id);

    }
}
