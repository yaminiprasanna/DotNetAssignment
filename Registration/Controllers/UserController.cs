using Common.Models;
using EFlib.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Types;

namespace Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {    
        private readonly IUser m_ApiService;
        public UserController(IUser apiService)
        {
            m_ApiService = apiService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> EmployeeRegister([FromBody] UserDTO model)
        {
            var res = await m_ApiService.RegisterUser(model);
            return Ok(res);

        }

       
    }
}
