using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IServices
{
    public interface IAlternateComponentService
    {
        Task<List<AlternateComponent>> GetAllAsync();
        Task<List<AlternateComponent>> GetByModelIdAsync(int modelId);
        Task<AlternateComponent> GetByIdAsync(int altId);
        Task<AlternateComponent> CreateAsync(AlternateComponent alternateComponent);
        Task<Dictionary<int, List<AlternateComponent>>> GetGroupedAlternatesByModelAsync(int modelId);
        Task<List<AlternateComponent>> GetByModelIdAndCompIdAsync(int modelId, int compId);
    }
}

