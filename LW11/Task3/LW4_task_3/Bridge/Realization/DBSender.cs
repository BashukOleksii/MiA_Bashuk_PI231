using LW4_task_3.Models.Entities;
using LW4_task_3.Interface.InterfacesRepository;

namespace LW4_task_3.Bridge.Realization
{
    public class DBSender : IMessageSender
    {
        private IMessageRepository _messageRepository;

        public DBSender(IMessageRepository messageRepository) 
        {
            _messageRepository = messageRepository;
        }

        public async Task SendAsync(MessageItem message) =>
            await _messageRepository.CreateAsync(message);
                          
    }
}
