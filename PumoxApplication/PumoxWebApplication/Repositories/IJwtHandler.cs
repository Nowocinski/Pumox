namespace PumoxWebApplication.Repositories
{
    public interface IJwtHandler
    {
        string CreateToken(string username, string password);
    }
}
