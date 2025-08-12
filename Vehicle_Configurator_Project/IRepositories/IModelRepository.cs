using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IRepositories
{
    public interface IModelRepository
    {
        Task<List<Model>> GetAllAsync();
        Task<Model> AddAsync(Model model);
        Task<Model> GetByIdAsync(int modelId);
        Task<List<Model>> GetBySegmentAsync(Segment segment);
        Task<List<Model>> GetByManufacturerAndSegmentAsync(Manufacturer manufacturer, Segment segment);
        Task<Model> GetModelWithDefaultComponentsAsync(int modelId);
        Task<decimal?> GetBasePriceByModelIdAsync(int modelId);
    }
}
