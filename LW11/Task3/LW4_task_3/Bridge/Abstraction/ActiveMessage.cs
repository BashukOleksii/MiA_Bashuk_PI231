using LW4_task_3.Bridge.Realization;
using LW4_task_3.Models.Entities;

namespace LW4_task_3.Bridge.Abstraction
{
    public class ActiveMessage : Message
    {
        public ActiveMessage(IMessageSender sender) : base(sender){ }

        public override async Task Send(SubscriptionItem sub)
        {
            var message = new MessageItem
            {
                Title = $"Підписка {sub.Service} активована",
                OwnerId = sub.OwnerId,
                SubId = sub.Id
            };

            await sender.SendAsync(message);
        }
    }
}
