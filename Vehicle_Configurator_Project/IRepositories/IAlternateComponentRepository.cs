using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IRepositories
{
    public interface IAlternateComponentRepository
    {
        Task<List<AlternateComponent>> GetAllAsync();
        Task<List<AlternateComponent>> GetByModelIdAsync(int modelId);
        Task<AlternateComponent> GetByIdAsync(int altId);
        Task<AlternateComponent> CreateAsync(AlternateComponent alternateComponent);
    }
}
