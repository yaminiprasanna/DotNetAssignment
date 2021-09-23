using Common.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly ILogin m_ApiService;
        public LoginController(ILogin loginService)
        {
            m_ApiService = loginService;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await m_ApiService.Authenticate(model.EmployeeName, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
    }
}
