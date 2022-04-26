using DapperSampleCodeWebApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperSampleCodeWebApp.Data
{
    public interface IUsersRepository : IDisposable
    {
        List<Users> GetAllUsers();
        Users GetUser(string username, string password);
    }
}
