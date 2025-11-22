using AutoMapper;
using LW4_task_3.Interface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LW4_task_3.Command.Invoker;
using LW4_task_3.Models.Request;
using LW4_task_3.Command.Commands;
using LW4_task_3.Models.Entities;
using System.Text.Json;

namespace LW4_task_3.Command.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly IPeopleService _peopleService;
        private readonly IMapper _mapper;
        private readonly PeopleInvoker _invoker;

        public CommandController(IPeopleService peopleService, IMapper mapper, PeopleInvoker invoker)
        {
            _peopleService = peopleService;
            _mapper = mapper;
            _invoker = invoker;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? name, string? email)
        {
            try
            {
                var peoples = await _peopleService.GetPeoplesItemsAsync(name, email);
                return Ok(peoples);
            }
            catch(KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var people = await _peopleService.GetByIdAsync(id);

                return Ok(people);
            }
            catch(KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create(PeopleRequest peopleRequest)
        {
            var people = _mapper.Map<PeopleItem>(peopleRequest);
            await _invoker.ExecuteAsync(new CreateCommand(_peopleService, people));

            return CreatedAtAction(nameof(GetById), new { Id = people.Id }, people);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, PeopleRequest peopleRequest)
        {
            var people = _mapper.Map<PeopleItem>(peopleRequest);

            try
            {
                await _invoker.ExecuteAsync(new UpdateCommand(_peopleService, people, id));

                return NoContent();
            }
            catch(KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePart(string id, JsonElement jsonElement)
        {
            try
            {
                var people = await _peopleService.GetByIdAsync(id);

                var updatePeople = _mapper.Map<PeopleItem>(people);

                if (jsonElement.TryGetProperty("name", out var name))
                    updatePeople.Name = name.ToString();
                if(jsonElement.TryGetProperty("email", out var email))
                    updatePeople.Email = email.ToString();

                if (!TryValidateModel(updatePeople))
                    return BadRequest();

                await _invoker.ExecuteAsync(new UpdateCommand(_peopleService, updatePeople, id));

                return NoContent();
            }
            catch(KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _invoker.ExecuteAsync(new DeleteCommand(_peopleService, id));

                return NoContent();
            }
            catch (KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }
        }

        [HttpPost("undo")]
        public async Task<IActionResult> Reset()
        {
            try
            {
                await _invoker.UndoAsync();

                return Ok("Успішно");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        


    }
}
