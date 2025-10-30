using LW4_task_3.Models;

namespace LW4_task_3.Interfaces
{
    public interface ISubService: IService<SubscriptionItem>
    {
        Task<IEnumerable<SubscriptionItem>> GetSubscriptionsItemsAsync(string? ownerId, string? service, SubStatus? subStatus);

    }
}
    