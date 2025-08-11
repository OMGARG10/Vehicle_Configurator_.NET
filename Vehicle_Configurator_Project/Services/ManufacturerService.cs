using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Models;
using Vehicle_Configurator_Project.Repositories;

namespace Vehicle_Configurator_Project.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository repository;

        public ManufacturerService(IManufacturerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<Manufacturer>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<List<Manufacturer>> GetBySegmentIdAsync(int segId)
        {
            return await repository.GetBySegmentIdAsync(segId);
        }
    }
}
