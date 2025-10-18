using LW_4_2.Data;
using LW_4_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Text.Json;

namespace LW_4_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<SubscriptionItems>> GetAll(
            [FromQuery] int? ownerId, [FromQuery] string? service, [FromQuery] SubStatus? subStatus)
        {
            IEnumerable<SubscriptionItems> subItems = SubData.subsripptionItems;

            if (ownerId is not null)
                subItems = subItems.Where(x => x.OwnerId == ownerId);
            if (service is not null)
                subItems = subItems.Where(x => x.Service == service);
            if (subStatus is not null)
                subItems = subItems.Where(x => x.Status == subStatus);

            if (subItems.Count() == 0)
                return NotFound("Не знайдено елементів за запитом");

            return Ok(subItems);

        }

        [HttpGet("{id}")]
        public ActionResult<SubscriptionItems> GetById(int id)
        {
            var sub = SubData.subsripptionItems.FirstOrDefault(x => x.Id == id);

            if (sub is null)
                return NotFound($"Не знайдено підписки з Id {id}");

            return Ok(sub);
        }

        [HttpPost]
        public ActionResult<SubscriptionItems> Create(SubscriptionItems item)
        {
            item.Id = SubData.subsripptionItems.Max(x => x.Id) + 1;

            SubData.subsripptionItems.Add(item);

            return CreatedAtAction(nameof(GetById), new { Id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, SubscriptionItems item)
        {
            var sub = SubData.subsripptionItems.FirstOrDefault(x => x.Id == id);

            if (sub is null)
                return NotFound($"Не знайдено підписки з Id {id}");

            sub.OwnerId = item.OwnerId;
            sub.Service = item.Service;
            sub.Status = item.Status;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePart(int id, JsonElement item)
        {
            var sub = SubData.subsripptionItems.FirstOrDefault(x => x.Id == id);

            if (sub is null)
                return NotFound($"Не знайдено підписки з Id {id}");

            var temp = new SubscriptionItems
            {
                Id = sub.Id,
                OwnerId = sub.OwnerId,
                Service = sub.Service,
                Status = sub.Status
            };

            if(item.TryGetProperty("ownerId",out var upId))
                temp.OwnerId = upId.GetInt32();
           
            if (item.TryGetProperty("status",out var upStatus))
                temp.Status = (SubStatus)upStatus.GetInt32();

            if (item.TryGetProperty("service",out var upServ))
                temp.Service = upServ.GetString();

            TryValidateModel(temp);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            sub.OwnerId = temp.OwnerId;
            sub.Service = temp.Service;
            sub.Status = temp.Status;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sub = SubData.subsripptionItems.FirstOrDefault(x => x.Id == id);

            if (sub is null)
                return NotFound($"Не знайдено підписки з Id {id}");

            SubData.subsripptionItems.Remove(sub);

            return NoContent();
        }

    }
}
