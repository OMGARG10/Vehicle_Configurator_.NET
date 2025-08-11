using Vehicle_Configurator_Project.Models;
using Vehicle_Configurator_Project.Repositories;
namespace Vehicle_Configurator_Project.Services
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;

        public ModelService(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<List<Model>> GetAllModelsAsync()
        {
            return await _modelRepository.GetAllAsync();
        }
    }
}
