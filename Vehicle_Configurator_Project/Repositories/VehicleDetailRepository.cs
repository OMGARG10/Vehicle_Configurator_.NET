using System;
using Microsoft.EntityFrameworkCore;
using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Repositories
{
    public class VehicleDetailRepository : IVehicleDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleDetail>> GetAllAsync()
        {
            return await _context.VehicleDetails
                .Include(v => v.Model)
                .Include(v => v.Component)
                .ToListAsync();
        }

        public async Task<List<VehicleDetail>> GetByModelIdAsync(int modelId)
        {
            return await _context.VehicleDetails
                .Include(v => v.Model)
                .Include(v => v.Component)
                .Where(v => v.ModelId == modelId)
                .ToListAsync();
        }

        public async Task<VehicleDetail> GetByIdAsync(int configId)
        {
            return await _context.VehicleDetails
                .Include(v => v.Model)
                .Include(v => v.Component)
                .FirstOrDefaultAsync(v => v.ConfigId == configId);
        }

        public async Task<VehicleDetail> CreateAsync(VehicleDetail vehicleDetail)
        {
            _context.VehicleDetails.Add(vehicleDetail);
            await _context.SaveChangesAsync();
            return vehicleDetail;
        }
    }

}

