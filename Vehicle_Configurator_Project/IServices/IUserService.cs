using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IServices
{
    public interface IUserService
    {
        Task<User> RegisterAsync(User user);
        Task<string> LoginAsync(string email, string password); 
    }
}
