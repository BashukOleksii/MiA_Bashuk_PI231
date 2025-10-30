using LW4_task_3.Interfaces;
using LW4_task_3.Models;
using LW4_task_3.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace LW4_task_3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
          _messageService = messageService;   
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageItem>>> GetAll(
            [FromQuery]string? ownerId, [FromQuery]string? subId, [FromQuery]string? title)
        {
            try
            {
                var messages = await _messageService.GetMessageItemsAsync(ownerId, subId, title);
                return Ok(messages);
            }
            catch(KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageItem>> GetById(string id)
        {
            try
            {
                ValidElement.ValidId(id);
                var message = await _messageService.GetByIdAsync(id);
                return Ok(message);
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

        [HttpPost]
        public async Task<IActionResult> Create(MessageItem element)
        {
            try
            {
                ValidElement.ValidId(element.OwnerId);
                ValidElement.ValidId(element.SubId);
                await _messageService.CreateAsync(element);

                return CreatedAtAction(nameof(GetById), new { Id = element.Id }, element);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, MessageItem element)
        {
            try
            {
                ValidElement.ValidId(id);
                ValidElement.ValidId(element.OwnerId);
                ValidElement.ValidId(element.SubId);
                await _messageService.UpdateAsync(id, element);

                return NoContent();
            }
            catch(KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
            catch (ArgumentException aex)
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
                var message = await _messageService.GetByIdAsync(id);

                var temp = new MessageItem
                {
                    Id = id,
                    OwnerId = message.OwnerId,
                    SubId = message.SubId,
                    Title = message.Title
                };

                if (element.TryGetProperty("ownerId", out var ownerId))
                {
                    temp.OwnerId = ownerId.GetString();
                    ValidElement.ValidId(ownerId.GetString());
                }
                if (element.TryGetProperty("subId", out var subId))
                {
                    temp.SubId = subId.GetString();
                    ValidElement.ValidId(subId.GetString());
                }
                if (element.TryGetProperty("title", out var title))
                    temp.Title = title.GetString();

                if(!TryValidateModel(temp))
                    return BadRequest(ModelState);

                await _messageService.UpdateAsync(id, temp);

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
                await _messageService.DeleteAsync(id);

                return NoContent();
            }
            catch(KeyNotFoundException kex)
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
