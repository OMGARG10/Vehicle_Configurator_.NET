using Microsoft.EntityFrameworkCore;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly ApplicationDbContext context;

        public ModelRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Model>> GetAllAsync()
        {
            return await context.Models
                .Include(m => m.Manufacturer)
                .Include(m => m.Segment)
                .Include(m => m.DefaultComponents)
                    .ThenInclude(vc => vc.Component)
                .ToListAsync();
        }
    }
}
