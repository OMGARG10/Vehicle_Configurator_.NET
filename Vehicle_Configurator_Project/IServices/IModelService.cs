using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IServices
{
    public interface IModelService
    {
        Task<List<Model>> GetAllModelsAsync();
        Task<Model> CreateModelAsync(Model model);
        Task<Model> GetModelByIdAsync(int modelId);
        Task<List<Model>> GetModelsBySegmentAsync(Segment segment);
        Task<List<Model>> GetModelsByManufacturerAndSegmentAsync(Manufacturer manufacturer, Segment segment);
        Task<Model> GetModelWithDefaultComponentsAsync(int modelId);
        Task<List<VehicleDetail>> GetComponentsByModelIdAsync(int modelId);
        Task<decimal?> GetBasePriceByModelIdAsync(int modelId);
        Task<Dictionary<char, Dictionary<int, List<AlternateComponent>>>> GetGroupedAlternatesByModelAsync(int modelId);
    }
}
