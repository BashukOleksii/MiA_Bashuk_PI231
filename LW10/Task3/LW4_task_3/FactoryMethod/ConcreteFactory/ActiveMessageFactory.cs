using LW4_task_3.FactoryMethod.Interface;
using LW4_task_3.Models.Entities;
namespace LW4_task_3.FactoryMethod.ConcreteFactory
{
    public class ActiveMessageFactory : IMessageFactory
    {
        public MessageItem CreateMessage(SubscriptionItem sub) =>
            new MessageItem
            {
                Title = $"Підписка {sub.Service} активована",
                SubId = sub.Id,
                OwnerId = sub.OwnerId
            };
       
    }
}
