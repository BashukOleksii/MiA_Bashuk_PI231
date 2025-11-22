    using LW4_task_3.Enums;
    using LW4_task_3.Interface.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using System.Security.Claims;

    namespace LW4_task_3.Controllers
    {
        [Route("[controller]")]
        [ApiController]
        public class TestController : ControllerBase
        {
            IUserService _userService;

            public TestController(IUserService userService)
            {
                _userService = userService;
            }
        
            [Authorize]
            [HttpGet("private")]
            public async Task<IActionResult> GetIdEmail()
            {
                var email = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                var user = await _userService.GetByEmailAsync(email);

                return Ok(new
                {
                    UserId = user.Id,
                    Email = email,
                });
            }

           
        }
    }
