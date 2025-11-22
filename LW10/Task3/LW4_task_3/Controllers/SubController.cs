using AutoMapper;
using LW4_task_3.Enums;
using LW4_task_3.Interface.Interfaces;
using LW4_task_3.Models.Entities;
using LW4_task_3.Models.Request;
using LW4_task_3.Models.Response;
using LW4_task_3.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LW4_task_3.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class SubController : ControllerBase
    {
        private readonly ISubService _subService;
        private readonly IMapper _mapper;

        public SubController(ISubService subService, IMapper mapper)
        {
            _subService = subService;
            _mapper = mapper;
        }

        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Manager)},{nameof(UserRole.User)}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionResponse>>> GetAll(
            [FromQuery] string? ownerId, [FromQuery] string? service, [FromQuery] SubStatus? status)
        {
            try
            {
                var subsItem = await _subService.GetSubscriptionsItemsAsync(ownerId, service, status);
                var subs = _mapper.Map<IEnumerable<SubscriptionResponse>>(subsItem);
                return Ok(subs);
            }
            catch (KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
        }

        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Manager)},{nameof(UserRole.User)}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionResponse>> GetById(string id)
        {
            try
            {
                ValidElement.ValidId(id);
                var subItem = await _subService.GetByIdAsync(id);
                var sub = _mapper.Map<SubscriptionResponse>(subItem);
                return Ok(sub);
            }
            catch (KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
            catch(ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
            
        }

        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Manager)}")]
        [HttpPost]
        public async Task<IActionResult> Create(SubscriptionRequest item)
        {
            try
            {
                var element = _mapper.Map<SubscriptionItem>(item);
                await _subService.CreateAsync(element);

                var resp = _mapper.Map<SubscriptionResponse>(element);

                return CreatedAtAction(nameof(GetById), new { Id = resp.Id},resp);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
        }

        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Manager)}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, SubscriptionRequest item)
        {
            try
            {
                ValidElement.ValidId(id);
                var element = _mapper.Map<SubscriptionItem>(item);
                await _subService.UpdateAsync(id, element);
                return NoContent();
            }
            catch(KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
            catch(ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
           
        }

        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Manager)}")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePart(string id, JsonElement element)
        {
            try
            {
                ValidElement.ValidId(id);
                var sub = await _subService.GetByIdAsync(id);

                var temp = _mapper.Map<SubscriptionRequest>(sub);

                if (element.TryGetProperty("ownerId", out var ownerId))
                    temp.OwnerId = ownerId.GetString();
                if (element.TryGetProperty("status", out var status))
                    temp.Status = status.GetString();
                if (element.TryGetProperty("service", out var service))
                    temp.Service = service.GetString();

                if(!TryValidateModel(temp))
                    return BadRequest(ModelState);

                _mapper.Map(temp, sub);
                await _subService.UpdateAsync(id, sub);

                return NoContent();
            }
            catch (KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
        }

        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Manager)}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                ValidElement.ValidId(id);
                await _subService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
        }

    }


}
