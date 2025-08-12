using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IServices
{
    public interface IVehicleDetailService
    {
        Task<List<VehicleDetail>> GetAllAsync();
        Task<List<VehicleDetail>> GetByModelIdAsync(int modelId);
        Task<VehicleDetail> GetByIdAsync(int configId);
        Task<VehicleDetail> CreateAsync(VehicleDetail vehicleDetail);

        Task<List<VehicleDetail>> GetByModelIdAndIsConfigurableAsync(int modelId, char isConfigurable);
    }
}
