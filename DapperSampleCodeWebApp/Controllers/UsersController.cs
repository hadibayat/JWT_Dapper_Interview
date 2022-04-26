using DapperSampleCodeWebApp.Class;
using DapperSampleCodeWebApp.Data;
using DapperSampleCodeWebApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace DapperSampleCodeWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository iusersRepository;
        private readonly IJWTManagerRepository _jWTManager;

        public UsersController(IConfiguration iconfiguration, IJWTManagerRepository jWTManager, IUsersRepository iusersRepository)
        {
            this._jWTManager = jWTManager;
            this.iusersRepository = iusersRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            string username = _jWTManager.ValidateJwtToken(token);
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized();
            }

            return Ok(iusersRepository.GetAllUsers());
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(Users userdata)
        {
            var token = _jWTManager.Authenticate(userdata);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
