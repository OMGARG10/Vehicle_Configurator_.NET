using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IRepositories
{
    public interface IComponentRepository
    {
        Task<List<Component>> GetAllAsync();
        Task<Component> GetByIdAsync(int compId);
        Task<Component> GetByNameAsync(string compName);
        Task<Component> CreateAsync(Component component);
    }
}
