using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IRepositories
{
    public interface IManufacturerRepository
    {
        Task<List<Manufacturer>> GetAllAsync();
        Task<List<Manufacturer>> GetBySegmentIdAsync(int segId);
        Task<Manufacturer> GetByIdAsync(int id);
    }
}
