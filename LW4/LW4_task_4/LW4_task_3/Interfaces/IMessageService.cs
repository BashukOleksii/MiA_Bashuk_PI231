using LW4_task_3.Models;

namespace LW4_task_3.Interfaces
{
    public interface IMessageService: IService<MessageItem>
    {
        Task<IEnumerable<MessageItem>> GetMessageItemsAsync(string? ownerId, string? subId, string? title);
    }
}
