using LW4_task_3.Models.Entities;

namespace LW4_task_3.FactoryMethod.Interface
{
    public interface IMessageFactory
    {
        MessageItem CreateMessage(SubscriptionItem sub);
    }
        
}
