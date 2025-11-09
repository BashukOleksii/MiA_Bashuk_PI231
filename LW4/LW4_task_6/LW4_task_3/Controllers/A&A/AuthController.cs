using AutoMapper;
using LW4_task_3.Interface.Interfaces;
using LW4_task_3.Models.Entities;
using LW4_task_3.Models.Request;
using LW4_task_3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace LW4_task_3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly JwtTokenGenerator _jwtGenerator;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, 
            IPasswordHasher passwordHasher, 
            JwtTokenGenerator JwtTokenGenerator, IMapper mapper)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _jwtGenerator = JwtTokenGenerator;
            _mapper = mapper;
        }
            
        [HttpPost("register")]
        public async Task<IActionResult> Reg(UserRegistration user)
        {
            var userItem = _mapper.Map<UserItem>(user);
            try
            {
                await _userService.CreateAsync(userItem);
                return Ok("Користувача зареєстровано");
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LoginModel loginModel)
        {
            string message = "Невірна пошта чи пароль";
            UserItem user;

            try
            {
                user = await _userService.GetByEmailAsync(loginModel.Email);
            }
            catch (KeyNotFoundException)
            {
                return Unauthorized(message);
            }

            if(!_passwordHasher.Verify(loginModel.Password,user.Password))
                return BadRequest(message);

            var token = _jwtGenerator.GenerateToken(user);

            user.RefreshToken = _jwtGenerator.GeneretaRefreshToken();
            user.RefreshTokenExpireTime = DateTime.UtcNow.AddDays(7);

            await _userService.UpdateAsync(user.Id, user);
                

            return Ok(new
            {
                Token = token,
                TokenExpireTime = DateTime.UtcNow.AddMinutes(60),
                ResreshToken = user.RefreshToken,
                ResreshTokenExpireTime = user.RefreshTokenExpireTime
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromHeader(Name = "Authorization")] string authHead, string refreshToken)
        {

            if (string.IsNullOrEmpty(authHead) || !authHead.StartsWith("Bearer "))
                return BadRequest("Немає або неправильний заголовок");

            var token = authHead.Substring("Bearer ".Length);

            var princial = _jwtGenerator.GetClaimsPrincipalFromExpiredToken(token);

            if (princial is null)
                return Unauthorized("Невірний token");

            var id = princial?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            UserItem user;
            try
            {
                user = await _userService.GetByIdAsync(id);
            }
            catch(KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }

            if (user.RefreshToken != refreshToken
                || user.RefreshTokenExpireTime <= DateTime.UtcNow)
                return Unauthorized("Невірний RefreshToken");

            var newToken = _jwtGenerator.GenerateToken(user);
            var newrefreshToken = _jwtGenerator.GeneretaRefreshToken();

            user.RefreshToken = newrefreshToken;
            user.RefreshTokenExpireTime = DateTime.UtcNow.AddDays(7);

            await _userService.UpdateAsync(id, user);

            return Ok(new
            {
                Token = token,
                TokenExpireTime = DateTime.UtcNow.AddMinutes(60),
                ResreshToken = user.RefreshToken,
                ResreshTokenExpireTime = user.RefreshTokenExpireTime
            });


        }
    }
}
