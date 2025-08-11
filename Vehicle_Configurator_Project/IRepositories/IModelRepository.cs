using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Repositories
{
    public interface IModelRepository
    {
        Task<List<Model>> GetAllAsync();
    }
}
