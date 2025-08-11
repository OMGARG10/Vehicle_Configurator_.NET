using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IServices
{
    public interface IComponentService
    {
        Task<List<Component>> GetAllComponentsAsync();
        Task<Component> GetComponentByIdAsync(int compId);
        Task<Component> GetComponentByNameAsync(string compName);
        Task<Component> CreateComponentAsync(Component component);
    }
}
