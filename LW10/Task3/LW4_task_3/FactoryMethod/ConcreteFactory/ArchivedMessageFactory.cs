using LW4_task_3.FactoryMethod.Interface;
using LW4_task_3.Models.Entities;

namespace LW4_task_3.FactoryMethod.ConcreteFactory
{
    public class ArchivedMessageFactory : IMessageFactory
    {
        public MessageItem CreateMessage(SubscriptionItem sub) =>
            new MessageItem
            {
                Title = $"Підписку {sub.Service} додано в архів",
                SubId = sub.Id,
                OwnerId = sub.OwnerId
            };

    }
}
