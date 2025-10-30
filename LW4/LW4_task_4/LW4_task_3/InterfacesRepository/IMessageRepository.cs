using LW4_task_3.Models;

namespace LW4_task_3.InterfacesRepository
{
    public interface IMessageRepository: IRepository<MessageItem>
    {
        Task<IEnumerable<MessageItem>> GetMessageItemsAsync(string?title, string? ownerId, string? subId);
    }
}
