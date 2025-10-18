using LW_4_2.Data;
using LW_4_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace LW_4_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController: ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<MessageItems>> GetAll(
            [FromQuery] int? ownerId, 
            [FromQuery] int? subId,
            [FromQuery] string? title)
        {
            IEnumerable<MessageItems> messages = MessageData.messageItems;

            if(ownerId is not null)
                messages = messages.Where(x => x.OwnerId == ownerId);
            if (subId is not null)
                messages = messages.Where(x => x.SubId == subId);
            if (title is not null)
                messages = messages.Where(x => x.Title == title);

            if (!messages.Any())
                return NotFound("Не знайдено елементів за вказаним запитом");

            return Ok(messages);

        }

        [HttpGet("{id}")]
        public ActionResult<MessageItems> GetById(int id)
        {
            var message = MessageData.messageItems.FirstOrDefault(x => x.Id == id);

            if (message is null)
                return NotFound($"Повідомлення із вказаним Id {id} не знайдено");

            return Ok(message);
        }

        [HttpPost]
        public ActionResult<MessageItems> Create(MessageItems message)
        {
            message.Id = MessageData.messageItems.Max(x => x.Id) + 1;

            MessageData.messageItems.Add(message);

            return CreatedAtAction(nameof(GetById), new { Id = message.Id}, message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, MessageItems message)
        {
            var mes = MessageData.messageItems.FirstOrDefault(x => x.Id == id);

            if (mes is null)
                return NotFound($"Повідомлення із вказаним Id {id} не знайдено");

            mes.OwnerId = message.OwnerId;
            mes.SubId = message.SubId;
            mes.Title = message.Title;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePart(int id,JsonElement message)
        {
            var mes = MessageData.messageItems.FirstOrDefault(x => x.Id == id);

            if (mes is null)
                return NotFound($"Повідомлення із вказаним Id {id} не знайдено");

            var temp = new MessageItems()
            {
                Id = id,
                OwnerId = mes.OwnerId,
                SubId = mes.SubId,
                Title = mes.Title,
            };

            if(message.TryGetProperty("ownerId",out var upO))
                temp.OwnerId = upO.GetInt32();

            if(message.TryGetProperty("subId", out var upS))
                temp.SubId = upS.GetInt32();

            if (message.TryGetProperty("title", out var upT))
                temp.Title = upT.GetString();
          

            TryValidateModel(temp);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            mes.OwnerId = temp.OwnerId;
            mes.SubId = temp.SubId;
            mes.Title = temp.Title;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var mes = MessageData.messageItems.FirstOrDefault(x => x.Id == id);

            if (mes is null)
                return NotFound($"Повідомлення із вказаним Id {id} не знайдено");

            MessageData.messageItems.Remove(mes);

            return NoContent();
        }

    }
}
