using LW4_task_3.FactoryMethod.Interface;
using LW4_task_3.Models.Entities;
using ZstdSharp.Unsafe;
using LW4_task_3.FactoryMethod.ConcreteFactory;

namespace LW4_task_3.FactoryMethod.Service
{
    public class MessageFactoryService
    {
        private readonly IServiceProvider _provider;

        public MessageFactoryService(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IMessageFactory GetFactory(SubStatus subStatus)
        {
            return subStatus switch
            {
                SubStatus.Expectation => _provider.GetRequiredService<ExpectationMessageFactory>(),
                SubStatus.Active => _provider.GetRequiredService<ActiveMessageFactory>(),
                SubStatus.Overdue => _provider.GetRequiredService<OverdueMessageFactory>(),
                SubStatus.Archived => _provider.GetRequiredService<ArchivedMessageFactory>()
            };
        }
    }
}
