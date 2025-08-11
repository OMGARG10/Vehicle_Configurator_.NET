using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Services
{
    public class VehicleDetailService : IVehicleDetailService
    {
        private readonly IVehicleDetailRepository _repository;

        public VehicleDetailService(IVehicleDetailRepository repository)
        {
            _repository = repository;
        }

        public Task<List<VehicleDetail>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<List<VehicleDetail>> GetByModelIdAsync(int modelId)
        {
            return _repository.GetByModelIdAsync(modelId);
        }

        public Task<VehicleDetail> GetByIdAsync(int configId)
        {
            return _repository.GetByIdAsync(configId);
        }

        public Task<VehicleDetail> CreateAsync(VehicleDetail vehicleDetail)
        {
            return _repository.CreateAsync(vehicleDetail);
        }
    }
}
