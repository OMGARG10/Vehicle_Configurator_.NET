using Microsoft.EntityFrameworkCore;
using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Repositories
{
    public class ComponentRepository : IComponentRepository
    {
        private readonly ApplicationDbContext context;

        public ComponentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Component>> GetAllAsync()
        {
            return await context.Components.ToListAsync();
        }

        public async Task<Component> GetByIdAsync(int compId)
        {
            return await context.Components.FindAsync(compId);
        }

        public async Task<Component> GetByNameAsync(string compName)
        {
            return await context.Components.FirstOrDefaultAsync(c => c.CompName == compName);
        }

        public async Task<decimal> GetAlternateComponentPriceAsync(int compId)
        {
            var price = await context.AlternateComponents
                          .Where(ac => ac.AlternateComponentId == compId)
                          .Select(ac => ac.DeltaPrice ?? 0m)
                          .FirstOrDefaultAsync();

            return price;
        }

        public async Task<Component> CreateAsync(Component component)
        {
            context.Components.Add(component);
            await context.SaveChangesAsync();
            return component;
        }
    }
}
