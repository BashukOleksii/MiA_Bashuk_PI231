using LW4_task_3.Bridge.Realization;
using LW4_task_3.Models.Entities;

namespace LW4_task_3.Bridge.Abstraction
{
    public abstract class Message
    {
        protected IMessageSender sender;

        public Message(IMessageSender sender)
        {
            this.sender = sender;
        }

        public abstract  Task Send(SubscriptionItem sub);
    }
}
