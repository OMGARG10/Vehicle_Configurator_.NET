using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Services
{
    public class AlternateComponentService : IAlternateComponentService
    {
        private readonly IAlternateComponentRepository _repository;

        public AlternateComponentService(IAlternateComponentRepository repository)
        {
            _repository = repository;
        }

        public Task<List<AlternateComponent>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<List<AlternateComponent>> GetByModelIdAsync(int modelId)
        {
            return _repository.GetByModelIdAsync(modelId);
        }

        public Task<AlternateComponent> GetByIdAsync(int altId)
        {
            return _repository.GetByIdAsync(altId);
        }

        public Task<AlternateComponent> CreateAsync(AlternateComponent alternateComponent)
        {
            return _repository.CreateAsync(alternateComponent);
        }
    }
}
