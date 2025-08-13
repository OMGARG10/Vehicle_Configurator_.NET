namespace Vehicle_Configurator_Project.Token
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string email);

    }
}
