using Vehicle_Configurator_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vehicle_Configurator_Project.IRepositories
{
    public interface IVehicleDetailRepository
    {
        Task<List<VehicleDetail>> GetAllAsync();
        Task<List<VehicleDetail>> GetByModelIdAsync(int modelId);
        Task<VehicleDetail> GetByIdAsync(int configId);
        Task<VehicleDetail> CreateAsync(VehicleDetail vehicleDetail);

        Task<List<VehicleDetail>> GetByModelIdAndIsConfigurableAsync(int modelId, char isConfigurable);

    }
}
