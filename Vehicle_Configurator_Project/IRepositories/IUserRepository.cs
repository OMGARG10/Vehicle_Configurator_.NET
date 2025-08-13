using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> AddUserAsync(User user);
        Task<User> GetByIdAsync(int userId);
    }
}
