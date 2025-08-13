using System;
using Microsoft.EntityFrameworkCore;
using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Repositories
{
    public class InvoiceHeaderRepository : IInvoiceHeaderRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceHeaderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<InvoiceHeader> AddInvoiceAsync(InvoiceHeader invoice)
        {
            await _context.InvoiceHeaders.AddAsync(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }
        public async Task AddAsync(InvoiceHeader invoice)
        {
            await _context.InvoiceHeaders.AddAsync(invoice);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<InvoiceHeader?> GetByIdAsync(int id)
        {
            return await _context.InvoiceHeaders
                .Include(i => i.User) // if User holds customer info
                .Include(i => i.InvoiceDetails)
                    .ThenInclude(d => d.Component)
                .FirstOrDefaultAsync(i => i.InvId == id);
        }
    }
}
