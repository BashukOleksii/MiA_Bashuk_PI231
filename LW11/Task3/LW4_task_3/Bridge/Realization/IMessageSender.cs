using LW4_task_3.Models.Entities;

namespace LW4_task_3.Bridge.Realization
{
    public interface IMessageSender
    {
        Task SendAsync(MessageItem message);
    }
}
