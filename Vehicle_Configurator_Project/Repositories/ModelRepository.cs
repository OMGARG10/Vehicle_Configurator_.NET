using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.Models;
using System.Linq;

namespace Vehicle_Configurator_Project.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly ApplicationDbContext _context;

        public ModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Model>> GetAllAsync()
        {
            return await _context.Models
                .Include(m => m.Manufacturer)
                .Include(m => m.Segment)
                .Include(m => m.DefaultComponents)
                    .ThenInclude(vd => vd.Component)
                .ToListAsync();
        }

        public async Task<Model> AddAsync(Model model)
        {
            _context.Models.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Model> GetByIdAsync(int modelId)
        {
            return await _context.Models
                .Include(m => m.Manufacturer)
                .Include(m => m.Segment)
                .Include(m => m.DefaultComponents)
                    .ThenInclude(vd => vd.Component)
                .FirstOrDefaultAsync(m => m.ModelId == modelId);
        }

        public async Task<List<Model>> GetBySegmentAsync(Segment segment)
        {
            return await _context.Models
                .Include(m => m.Manufacturer)
                .Include(m => m.Segment)
                .Include(m => m.DefaultComponents)
                    .ThenInclude(vd => vd.Component)
                .Where(m => m.SegmentId == segment.SegId)
                .ToListAsync();
        }

        public async Task<List<Model>> GetByManufacturerAndSegmentAsync(Manufacturer manufacturer, Segment segment)
        {
            return await _context.Models
                .Include(m => m.Manufacturer)
                .Include(m => m.Segment)
                .Include(m => m.DefaultComponents)
                    .ThenInclude(vd => vd.Component)
                .Where(m => m.ManufacturerId == manufacturer.MfgId && m.SegmentId == segment.SegId)
                .ToListAsync();
        }

        public async Task<Model> GetModelWithDefaultComponentsAsync(int modelId)
        {
            return await _context.Models
                .Include(m => m.Manufacturer)
                .Include(m => m.Segment)
                .Include(m => m.DefaultComponents)
                    .ThenInclude(vd => vd.Component)
                .FirstOrDefaultAsync(m => m.ModelId == modelId);
        }

        public async Task<decimal?> GetBasePriceByModelIdAsync(int modelId)
        {
            var model = await _context.Models.FindAsync(modelId);
            return model?.Price;
        }
    }
}
