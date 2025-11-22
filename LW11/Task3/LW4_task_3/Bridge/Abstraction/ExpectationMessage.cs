using LW4_task_3.Bridge.Realization;
using LW4_task_3.Models.Entities;

namespace LW4_task_3.Bridge.Abstraction
{

    public class ExpectationMessage : Message
    {
        public ExpectationMessage(IMessageSender sender) : base(sender) { }

        public override async Task Send(SubscriptionItem sub)
        {
            var message = new MessageItem
            {
                Title = $"Підписку {sub.Service} додано, але ще не активовано",
                OwnerId = sub.OwnerId,
                SubId = sub.Id
            };

            await sender.SendAsync(message);
        }
    }
}
