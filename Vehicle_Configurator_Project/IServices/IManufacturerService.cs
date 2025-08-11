using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IServices
{
    public interface IManufacturerService
    {
        Task<List<Manufacturer>> GetAllAsync();
        Task<List<Manufacturer>> GetBySegmentIdAsync(int segId);
    }
}
