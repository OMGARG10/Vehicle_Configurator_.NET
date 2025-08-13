using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vehicle_Configurator_Project.Services
{
    public class InvoiceDetailService : IInvoiceDetailService
    {
        private readonly IInvoiceDetailRepository _repository;

        public InvoiceDetailService(IInvoiceDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InvoiceDetail>> GetAllInvoiceDetailsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<InvoiceDetail> CreateInvoiceDetailAsync(InvoiceDetail detail)
        {
            return await _repository.AddAsync(detail);
        }

        public async Task<List<InvoiceDetail>> GetInvoiceDetailsByInvoiceIdAsync(int invoiceId)
        {
            // Use InvoiceHeaderId FK property to query
            return await _repository.GetByInvoiceIdAsync(invoiceId);
        }
    }
}
