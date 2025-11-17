using AutoMapper;
using LW4_task_3.Enums;
using LW4_task_3.Interface.Interfaces;
using LW4_task_3.Models.Entities;
using LW4_task_3.Models.Request;
using LW4_task_3.Models.Response;
using LW4_task_3.Repositories;
using LW4_task_3.Validators;
using Microsoft.AspNetCore.Authorization;
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
            private readonly IMapper _mapper;

            public MessageController(IMessageService messageService, IMapper mapper)
            {
              _messageService = messageService;   
              _mapper = mapper;
            }

        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Manager)},{nameof(UserRole.User)}")]
        [HttpGet]
            public async Task<ActionResult<IEnumerable<MessageResponse>>> GetAll(
                [FromQuery]string? ownerId, [FromQuery]string? subId, [FromQuery]string? title)
            {
                try
                {
                    var messagesItem = await _messageService.GetMessageItemsAsync(ownerId, subId, title);
                    var messages = _mapper.Map<IEnumerable<MessageResponse>>(messagesItem);
                    return Ok(messages);
                }
                catch(KeyNotFoundException kex)
                {
                    return NotFound(kex.Message);
                }
            }


        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Manager)},{nameof(UserRole.User)}")]
        [HttpGet("{id}")]
            public async Task<ActionResult<MessageResponse>> GetById(string id)
            {
                try
                {
                    ValidElement.ValidId(id);
                    var messageItem = await _messageService.GetByIdAsync(id);
                    var message = _mapper.Map<MessageResponse>(messageItem);
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

        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Manager)}")]
        [HttpPost]
            public async Task<IActionResult> Create(MessageRequest item)
            {
                try
                {
                    var element = _mapper.Map<MessageItem>(item);
                    await _messageService.CreateAsync(element);
                    var resp = _mapper.Map<MessageResponse>(element);
                    return CreatedAtAction(nameof(GetById), new { Id = resp.Id }, resp);
                }
                catch (ArgumentException aex)
                {
                    return BadRequest(aex.Message);
                }
            }
       
        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Manager)}")]
        [HttpPut("{id}")]
            public async Task<IActionResult> Update(string id, MessageRequest item)
            {
                try
                {
                    ValidElement.ValidId(id);
                    var element = _mapper.Map<MessageItem>(item);
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

        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Manager)}")]
        [HttpPatch("{id}")]
            public async Task<IActionResult> UpdatePart(string id, JsonElement element)
            {
                try
                {
                    ValidElement.ValidId(id);
                    var message = await _messageService.GetByIdAsync(id);

                    var temp = _mapper.Map<MessageRequest>(message);

                    if (element.TryGetProperty("ownerId", out var ownerId))
                        temp.OwnerId = ownerId.GetString();
                    if (element.TryGetProperty("subId", out var subId))
                        temp.SubId = subId.GetString();
                    if (element.TryGetProperty("title", out var title))
                        temp.Title = title.GetString();

                    if(!TryValidateModel(temp))
                        return BadRequest(ModelState);

                    _mapper.Map(temp, message);
                    await _messageService.UpdateAsync(id, message);

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
