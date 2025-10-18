    using LW_4_2.Data;
    using LW_4_2.Models;
    using Microsoft.AspNetCore.Mvc;


using System.Text.Json;

namespace LW_4_2.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class PeopleController : ControllerBase
        {
            [HttpGet]
            public ActionResult<IEnumerable<PeopleItems>> GetAll(
                [FromQuery] string? Name, [FromQuery] string? Email)
            {
                IEnumerable<PeopleItems> peoples = PeopleData.peopleItems;

                if(Name is not null)
                    peoples = peoples.Where(x => x.Name == Name);

                if(Email is not null)
                    peoples = peoples.Where(x => x.Email == Email);

                if (!peoples.Any())
                    return NotFound("Не знайдено елементів");

                return Ok(peoples);
            }

            [HttpGet("{id}")]
            public ActionResult<PeopleItems> GetById(int id)
            {
                var people = PeopleData.peopleItems.FirstOrDefault(x => x.Id == id);

                if (people is null)
                    return NotFound($"Не знайдено людину із вказаним 'Id': {id}");

                return Ok(people);
            }       
      

            [HttpPost]
            public ActionResult<PeopleItems> Create(PeopleItems item)
            {
                item.Id = PeopleData.peopleItems.Max(x => x.Id) + 1;

                PeopleData.peopleItems.Add(item);

                return CreatedAtAction(nameof(GetById), new { Id = item.Id }, item);

            }

            [HttpPut("{id}")]
            public IActionResult Update(int id, PeopleItems upPeople)
            {

                var people = PeopleData.peopleItems.FirstOrDefault(p => p.Id == id);

                if (people is null)
                    return NotFound($"Не знайдено людини із Id {id}");

                people.Name = upPeople.Name;
                people.Email = upPeople.Email;

                return NoContent();
            }

            [HttpPatch("{id}")]
            public IActionResult UpdatePart(int id, [FromBody] JsonElement upPeople)
            {

                ModelState.ClearValidationState(nameof(upPeople));
                var people = PeopleData.peopleItems.FirstOrDefault(x => x.Id == id);

                if(people is null)
                    return NotFound($"Не знайдено людини за вказаним Id {id}");
                    

            var temp = new PeopleItems
            {
                Id = id,
                Email = people.Email,
                Name = people.Name
            };
                
                
                if(upPeople.TryGetProperty("name",out var name))
                    temp.Name = name.GetString();
                if(upPeople.TryGetProperty("email",out var email))
                     temp.Email = email.GetString();

                if (!TryValidateModel(temp))
                return BadRequest(ModelState);

                people.Name = temp.Name;
                people.Email = temp.Email;
               return NoContent();
            }

            [HttpDelete("{id}")]
            public IActionResult DeletePart(int id) 
            {
                var people = PeopleData.peopleItems.FirstOrDefault(p => p.Id == id);

                if (people is null)
                    return NotFound($"Не знайдено людину із вказаним Id {id}");

                PeopleData.peopleItems.Remove(people);

                return NoContent(); 
            }

        }
    }
