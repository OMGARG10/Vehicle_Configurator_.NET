namespace Vehicle_Configurator_Project.Services
{
    using Microsoft.AspNetCore.Identity;
    using Vehicle_Configurator_Project.IRepositories;
    using Vehicle_Configurator_Project.IServices;
    using Vehicle_Configurator_Project.Models;
    using Vehicle_Configurator_Project.Token;

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<User> RegisterAsync(User user)
        {
            if (await _userRepository.GetByEmailAsync(user.Email) != null)
                throw new Exception("Email already registered");

            user.Password = _passwordHasher.HashPassword(user, user.Password);
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) throw new Exception("Invalid username or password");

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (result != PasswordVerificationResult.Success)
                throw new Exception("Invalid username or password");

            return _jwtService.GenerateToken(user.Email);
        }
    }

}
