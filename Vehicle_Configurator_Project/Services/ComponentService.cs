using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Services
{
    public class ComponentService : IComponentService
    {
        private readonly IComponentRepository _componentRepository;

        public ComponentService(IComponentRepository componentRepository)
        {
            _componentRepository = componentRepository;
        }

        public async Task<List<Component>> GetAllComponentsAsync()
        {
            return await _componentRepository.GetAllAsync();
        }

        public async Task<Component> GetComponentByIdAsync(int compId)
        {
            return await _componentRepository.GetByIdAsync(compId);
        }

        public async Task<Component> GetComponentByNameAsync(string compName)
        {
            return await _componentRepository.GetByNameAsync(compName);
        }

        public async Task<Component> CreateComponentAsync(Component component)
        {
            return await _componentRepository.CreateAsync(component);
        }
    }
}
