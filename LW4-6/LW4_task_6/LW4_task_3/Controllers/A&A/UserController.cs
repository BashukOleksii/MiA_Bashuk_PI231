using AutoMapper;
using LW4_task_3.Enums;
using LW4_task_3.Interface.Interfaces;
using LW4_task_3.Models.Entities;
using LW4_task_3.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Data;
using System.Security.Claims;
using ZstdSharp.Unsafe;

namespace LW4_task_3.Controllers.A_A
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public UserController(IUserService userService,IMapper mapper, IPasswordHasher passwordHasher)
        {
            _userService = userService;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Manager)}")]
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }


        }

        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Manager)}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneUser(string id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);

                return Ok(user);
            }
            catch(KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
        }
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UserRegistration userRegistration)
        {
            try
            {
                var updateUser = await _userService.GetByIdAsync(id);
                var emailUser = await _userService.GetByEmailAsync(userRegistration.Email);

                if (emailUser is not null && emailUser.Id != updateUser.Id)
                    return BadRequest("Ця пошта вже використовується");

                userRegistration.Password = _passwordHasher.Hash(userRegistration.Password);

                _mapper.Map(userRegistration, updateUser);

                await _userService.UpdateAsync(id, updateUser);

                return NoContent();

            }
            catch (KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
        }


        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                await _userService.DeleteAsync(id);

                return NoContent();
            }
            catch (KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("setRole")]
        public async Task<IActionResult> SetRole(string id, UserRole role)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);

                user.Role = role;

                await _userService.UpdateAsync(id, user);

                return Ok(new { UserName = user.UserName, Email = user.Email, Role = (int)user.Role });
            }
            catch (KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("addRole")]
        public async Task<IActionResult> AddRole(string id, UserRole role)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);

                user.Role |= role;

                await _userService.UpdateAsync(id, user);

                return Ok(new { UserName = user.UserName, Email = user.Email, Role = (int)user.Role });
            }
            catch (KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("removeRole")]
        public async Task<IActionResult> RemoveRole(string id, UserRole role)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);

                user.Role &= ~role;

                await _userService.UpdateAsync(id, user);

                return Ok(new { UserName = user.UserName, Email = user.Email, Role = (int)user.Role });
            }
            catch (KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
        }

    }
}
