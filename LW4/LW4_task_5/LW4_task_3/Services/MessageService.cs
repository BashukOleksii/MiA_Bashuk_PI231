using LW4_task_3.Clients;
using LW4_task_3.Interfaces;
using LW4_task_3.InterfacesRepository;
using LW4_task_3.Models.Entities;
using MongoDB.Driver;

namespace LW4_task_3.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ISubRepository _subRepository;
        private readonly IPeopleRepository _peopleRepository;
        public MessageService(IMessageRepository messageRepository, ISubRepository subRepository, IPeopleRepository peopleRepository)
        {
            _messageRepository = messageRepository;
            _subRepository = subRepository;
            _peopleRepository = peopleRepository;
        }
        public async Task CreateAsync(MessageItem element)
        {
            await ValidMessage(element.OwnerId,element.SubId);

            await _messageRepository.CreateAsync(element);
        }

        public async Task DeleteAsync(string id)
        {
            if (!await _messageRepository.IsExist(id))
                throw new KeyNotFoundException($"Не знайдено підписки за вказаним Id {id}");

            await _messageRepository.DeleteAsync(id);
        }

        public async Task<MessageItem> GetByIdAsync(string id)
        {
            var message = await _messageRepository.GetByIdAsync(id);

            if(message is null)
                throw new KeyNotFoundException($"Не знайдено підписки за вказаним Id {id}");

            return message;
        }

        public async Task<IEnumerable<MessageItem>> GetMessageItemsAsync(string? title,string? ownerId, string? subId)
        {
            var messages = await _messageRepository.GetMessageItemsAsync(title, ownerId, subId);

            if (messages is null || !messages.Any())
                throw new KeyNotFoundException("Не знайдено повідомлень");

            return messages;
        }
       
        public async Task UpdateAsync(string id, MessageItem element)
        {

            if(!await _messageRepository.IsExist(id))
                throw new KeyNotFoundException($"Не знайдено підписки за вказаним Id {id}");

            await ValidMessage(element.OwnerId, element.SubId);

            element.Id = id;

            await _messageRepository.UpdateAsync(id, element);

        }

        public async Task ValidMessage(string ownerId, string subId)
        {
            if (!await _peopleRepository.IsExist(ownerId))
                throw new ArgumentException($"Не знайдено користувача з id {ownerId}");

            if (!await _subRepository.IsExist(subId)) 
                throw new ArgumentException($"Не знайдено підписки з id {subId}");

            var sub = await _subRepository.GetByIdAsync(subId);

            if (sub.OwnerId != ownerId)
                throw new ArgumentException($"В підписки з id {subId} немає власника з id {ownerId}");
        }
    }
}
