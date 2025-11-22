using LW4_task_3.Bridge.Realization;
using LW4_task_3.Models.Entities;

namespace LW4_task_3.Bridge.Abstraction
{
    public class OverdueMessage : Message
    {
        public OverdueMessage(IMessageSender sender) : base(sender) { }

        public override async Task Send(SubscriptionItem sub)
        {
            var message = new MessageItem
            {
                Title = $"Підписка {sub.Service} прострочена",
                OwnerId = sub.OwnerId,
                SubId = sub.Id
            };

            await sender.SendAsync(message);
        }
    }
}
