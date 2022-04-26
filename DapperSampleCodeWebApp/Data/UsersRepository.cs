using DapperSampleCodeWebApp.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace DapperSampleCodeWebApp.Data
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IConfiguration iconfiguration;

        public UsersRepository(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        public List<Users> GetAllUsers()
        {
            using (var connection = new DapperContext(iconfiguration).CreateConnection())
            {
                var sql = "select * from Users";
                var result = connection.Query<Users>(sql).ToList();
                return result;
            }
        }

        public Users GetUser(string username, string password)
        {
            using (var connection = new DapperContext(iconfiguration).CreateConnection())
            {
                var parameters = new { UserName = username, Password = password };
                var sql = "select * from Users where UserName = @UserName and Password = @Password";
                var result = connection.Query<Users>(sql, parameters);
                if (result.Count() == 0)
                    return null;

                return result.First();
            }
        }

        public void Dispose()
        {

        }
    }
}
