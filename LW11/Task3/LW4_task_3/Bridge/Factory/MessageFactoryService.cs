using LW4_task_3.Bridge.Abstraction;
using LW4_task_3.Models.Entities;

namespace LW4_task_3.Bridge.Factory
{
    public class MessageFactoryService
    {
        private readonly IServiceProvider _serviceProvider;

        public MessageFactoryService(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider; 
        }

        public Message GetMessage(SubStatus status)
        {
            return status switch
            {
                SubStatus.Expectation => _serviceProvider.GetRequiredService<ExpectationMessage>(),
                SubStatus.Active => _serviceProvider.GetRequiredService<ActiveMessage>(),
                SubStatus.Overdue => _serviceProvider.GetRequiredService<OverdueMessage>(),
                SubStatus.Archived => _serviceProvider.GetRequiredService<ArchivedMessage>()
            };
        }
    }
}
