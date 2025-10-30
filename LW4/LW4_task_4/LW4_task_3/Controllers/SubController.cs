using LW4_task_3.Interfaces;
using LW4_task_3.Models;
using LW4_task_3.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LW4_task_3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubController : ControllerBase
    {
        private readonly ISubService _subService;

        public SubController(ISubService subService)
        {
            _subService = subService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionItem>>> GetAll(
            [FromQuery] string? ownerId, [FromQuery] string? service, [FromQuery] SubStatus? status)
        {
            try
            {
                var subs = await _subService.GetSubscriptionsItemsAsync(ownerId, service, status);
                return Ok(subs);
            }
            catch (KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionItem>> GetById(string id)
        {
            try
            {
                ValidElement.ValidId(id);
                var sub = await _subService.GetByIdAsync(id);
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

        [HttpPost]
        public async Task<IActionResult> Create(SubscriptionItem element)
        {
            try
            {
                await _subService.CreateAsync(element);
                return CreatedAtAction(nameof(GetById), new { Id = element.Id},element);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, SubscriptionItem element)
        {
            try
            {
                ValidElement.ValidId(id);
                ValidElement.ValidId(element.OwnerId);
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePart(string id, JsonElement element)
        {
            try
            {
                ValidElement.ValidId(id);
                var sub = await _subService.GetByIdAsync(id);

                var temp = new SubscriptionItem
                {
                    Id = id,
                    OwnerId = sub.OwnerId,
                    Service = sub.Service,
                    Status = sub.Status
                };

                if (element.TryGetProperty("ownerId", out var ownerId))
                {
                    temp.OwnerId = ownerId.GetString();
                    ValidElement.ValidId(ownerId.GetString());
                }
                if (element.TryGetProperty("status", out var status))
                    temp.Status = (SubStatus)status.GetInt32();
                if (element.TryGetProperty("service", out var service))
                    temp.Service = service.GetString();

                if(!TryValidateModel(temp))
                    return BadRequest(ModelState);

                await _subService.UpdateAsync(id, temp);

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
