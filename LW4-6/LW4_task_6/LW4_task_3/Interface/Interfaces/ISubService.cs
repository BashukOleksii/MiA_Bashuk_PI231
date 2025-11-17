using LW4_task_3.Models.Entities;

namespace LW4_task_3.Interface.Interfaces
{
    public interface ISubService: IService<SubscriptionItem>
    {
        Task<IEnumerable<SubscriptionItem>> GetSubscriptionsItemsAsync(string? ownerId, string? service, SubStatus? subStatus);

    }
}
    