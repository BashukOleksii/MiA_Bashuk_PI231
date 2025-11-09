using LW4_task_3.Models.Entities;

namespace LW4_task_3.Interface.Interfaces
{
    public interface IMessageService: IService<MessageItem>
    {
        Task<IEnumerable<MessageItem>> GetMessageItemsAsync(string? ownerId, string? subId, string? title);
    }
}
