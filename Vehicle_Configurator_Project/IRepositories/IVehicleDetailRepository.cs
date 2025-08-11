using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IRepositories
{
    public interface IVehicleDetailRepository
    {
        Task<List<VehicleDetail>> GetAllAsync();
        Task<List<VehicleDetail>> GetByModelIdAsync(int modelId);
        Task<VehicleDetail> GetByIdAsync(int configId);
        Task<VehicleDetail> CreateAsync(VehicleDetail vehicleDetail);
    }
}
