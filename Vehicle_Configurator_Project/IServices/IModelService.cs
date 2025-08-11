using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Services
{
    public interface IModelService
    {
        Task<List<Model>> GetAllModelsAsync();
    }
}
