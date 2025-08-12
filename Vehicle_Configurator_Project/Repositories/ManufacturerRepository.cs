using Microsoft.EntityFrameworkCore;
using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly ApplicationDbContext context;

        public ManufacturerRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Manufacturer>> GetAllAsync()
        {
            return await context.Manufacturers.ToListAsync();
        }

        public async Task<List<Manufacturer>> GetBySegmentIdAsync(int segId)
        {
            return await context.Manufacturers
                .Where(m => m.Models.Any(model => model.SegmentId == segId))
                .ToListAsync();
        }
        public async Task<Manufacturer> GetByIdAsync(int id)
        {
            return await context.Manufacturers.FindAsync(id);
        }
    }
}
