using LW4_task_3.Models;

namespace LW4_task_3.InterfacesRepository
{
    public interface ISubRepository: IRepository<SubscriptionItem>
    {
        Task<IEnumerable<SubscriptionItem>> GetSubscriptionsItemsAsync(string? ownerId, string? service, SubStatus? subStatus);
    }
}
