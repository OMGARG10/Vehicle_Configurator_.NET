using Microsoft.EntityFrameworkCore;
using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Repositories
{
    public class InvoiceDetailRepository : IInvoiceDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<InvoiceDetail>> GetAllAsync()
        {
            return await _context.InvoiceDetails
                .Include(i => i.Component) // Eager load Component
                .Include(i => i.InvoiceHeader) // Also eager load InvoiceHeader if needed
                .ToListAsync();
        }

        public async Task<InvoiceDetail> AddAsync(InvoiceDetail detail)
        {
            _context.InvoiceDetails.Add(detail);
            await _context.SaveChangesAsync();
            return detail;
        }

        public async Task<List<InvoiceDetail>> GetByInvoiceIdAsync(int invoiceId)
        {
            return await _context.InvoiceDetails
                .Where(i => i.InvoiceHeaderId == invoiceId)  // Filter by foreign key
                .Include(i => i.Component) // Load component info
                .Include(i => i.InvoiceHeader) // Load invoice header if needed
                .ToListAsync();
        }

        public async Task AddRangeAsync(IEnumerable<InvoiceDetail> details)
        {
            await _context.InvoiceDetails.AddRangeAsync(details);
            await _context.SaveChangesAsync();
        }
    }
}
