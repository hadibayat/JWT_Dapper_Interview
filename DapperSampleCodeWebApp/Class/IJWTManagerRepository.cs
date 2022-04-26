using DapperSampleCodeWebApp.Model;

namespace DapperSampleCodeWebApp
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
        string ValidateJwtToken(string token);
    }
}