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
    }
}

