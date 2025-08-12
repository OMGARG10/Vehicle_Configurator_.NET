using System;
using Microsoft.EntityFrameworkCore;
using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Repositories
{
    public class AlternateComponentRepository : IAlternateComponentRepository
    {
        private readonly ApplicationDbContext _context;

        public AlternateComponentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AlternateComponent>> GetAllAsync()
        {
            return await _context.AlternateComponents
                .Include(ac => ac.Model)
                .Include(ac => ac.BaseComponent)
                .Include(ac => ac.AlternateComponentEntity)
                .ToListAsync();
        }

        public async Task<List<AlternateComponent>> GetByModelIdAsync(int modelId)
        {
            return await _context.AlternateComponents
                .Include(ac => ac.Model)
                .Include(ac => ac.BaseComponent)
                .Include(ac => ac.AlternateComponentEntity)
                .Where(ac => ac.ModelId == modelId)
                .ToListAsync();
        }

        public async Task<AlternateComponent> GetByIdAsync(int altId)
        {
            return await _context.AlternateComponents
                .Include(ac => ac.Model)
                .Include(ac => ac.BaseComponent)
                .Include(ac => ac.AlternateComponentEntity)
                .FirstOrDefaultAsync(ac => ac.AltId == altId);
        }

        public async Task<AlternateComponent> CreateAsync(AlternateComponent alternateComponent)
        {
            _context.AlternateComponents.Add(alternateComponent);
            await _context.SaveChangesAsync();
            return alternateComponent;
        }
        public async Task<Dictionary<int, List<AlternateComponent>>> GetGroupedAlternatesByModelAsync(int modelId)
        {
            return await _context.AlternateComponents
                .Where(ac => ac.ModelId == modelId)
                .GroupBy(ac => ac.BaseComponentId)
                .ToDictionaryAsync(g => g.Key, g => g.ToList());
        }
        public async Task<List<AlternateComponent>> GetByModelIdAndCompIdAsync(int modelId, int compId)
        {
            var alternates = await _context.AlternateComponents
                .Include(a => a.Model)
                    .ThenInclude(m => m.Manufacturer)
                .Include(a => a.Model)
                    .ThenInclude(m => m.Segment)
                .Include(a => a.BaseComponent)
                .Include(a => a.AlternateComponentEntity)
                .Where(a => a.ModelId == modelId && a.BaseComponent.CompId == compId)
                .ToListAsync();

            return alternates;
        }
    }
}

