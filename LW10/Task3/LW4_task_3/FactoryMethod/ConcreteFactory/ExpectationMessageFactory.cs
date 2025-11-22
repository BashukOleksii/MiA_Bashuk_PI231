using LW4_task_3.FactoryMethod.Interface;
using LW4_task_3.Models.Entities;

namespace LW4_task_3.FactoryMethod.ConcreteFactory
{
    public class ExpectationMessageFactory : IMessageFactory
    {
        public MessageItem CreateMessage(SubscriptionItem sub) =>
            new MessageItem
            {
                Title = $"Підписка {sub.Service} створена, та ще не активована",
                SubId = sub.Id,
                OwnerId = sub.OwnerId
            };

    }
}
