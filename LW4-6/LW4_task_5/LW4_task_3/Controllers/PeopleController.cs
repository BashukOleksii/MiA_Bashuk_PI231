using AutoMapper;
using LW4_task_3.Interfaces;
using LW4_task_3.Models.Entities;
using LW4_task_3.Models.Request;
using LW4_task_3.Models.Response;
using LW4_task_3.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LW4_task_3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;
        private readonly IMapper _mapper;

        public PeopleController(IPeopleService peopleService, IMapper mapper)
        {
            _peopleService = peopleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeopleResponse>>> Get(
            [FromQuery] string? name, [FromQuery] string? email)
        {
            try
            {
                var peoplesItems = await _peopleService.GetPeoplesItemsAsync(name, email);
                var peoples = _mapper.Map<IEnumerable<PeopleResponse>>(peoplesItems);

                return Ok(peoples);
            }
            catch (KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }

        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<PeopleResponse>> GetById(string id)
        {
            try 
            {
                ValidElement.ValidId(id);
                var peopleItem = await _peopleService.GetByIdAsync(id);
                var people = _mapper.Map<PeopleResponse>(peopleItem);

                return Ok(people);
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
        public async Task<IActionResult> Create(PeopleRequest item)
        {
            var element = _mapper.Map<PeopleItem>(item);

            await _peopleService.CreateAsync(element);

            var resp = _mapper.Map<PeopleResponse>(element);

            return CreatedAtAction(nameof(GetById), new { Id = resp.Id }, resp);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, PeopleRequest item)
        {
            try
            {
                ValidElement.ValidId(id);
                var element = _mapper.Map<PeopleItem>(item);
                await _peopleService.UpdateAsync(id, element);
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePart(string id, JsonElement element)
        {
            try
            {
                ValidElement.ValidId(id);

                var upPeople = await _peopleService.GetByIdAsync(id);

                var temp = _mapper.Map<PeopleRequest>(upPeople);

                if (element.TryGetProperty("name", out var name))
                    temp.Name = name.GetString();
                if (element.TryGetProperty("email", out var email))
                    temp.Email = email.GetString();

                if (!TryValidateModel(temp))
                    return BadRequest(ModelState);

                _mapper.Map(temp, upPeople);
                await _peopleService.UpdateAsync(id, upPeople);

                return NoContent();
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                ValidElement.ValidId(id);

                await _peopleService.DeleteAsync(id);

                return NoContent();

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

    }
}
