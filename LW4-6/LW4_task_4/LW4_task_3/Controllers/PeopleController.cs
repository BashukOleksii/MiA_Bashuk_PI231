using LW4_task_3.Interfaces;
using LW4_task_3.Models;
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

        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeopleItem>>> Get(
            [FromQuery] string? name, [FromQuery] string? email)
        {
            try
            {
                var peoples = await _peopleService.GetPeoplesItemsAsync(name, email);
                return Ok(peoples);
            }
            catch (KeyNotFoundException kex)
            {
                return NotFound(kex.Message);
            }

        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<PeopleItem>> GetById(string id)
        {
            try 
            {
                ValidElement.ValidId(id);
                var people = await _peopleService.GetByIdAsync(id);
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
        public async Task<IActionResult> Create(PeopleItem element)
        {
            await _peopleService.CreateAsync(element);

            return CreatedAtAction(nameof(GetById), new { Id = element.Id }, element);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, PeopleItem element)
        {
            try
            {
                ValidElement.ValidId(id);
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

                PeopleItem temp = new PeopleItem
                {
                    Id = id,
                    Name = upPeople.Name,
                    Email = upPeople.Email,
                };

                if (element.TryGetProperty("name", out var name))
                    temp.Name = name.GetString();
                if (element.TryGetProperty("email", out var email))
                    temp.Email = email.GetString();

                if (!TryValidateModel(temp))
                    return BadRequest(ModelState);

                await _peopleService.UpdateAsync(id, temp);

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
