using LW4_task_3.Models.Entities;

namespace LW4_task_3.Interface.InterfacesRepository
{
    public interface ISubRepository: IRepository<SubscriptionItem>
    {
        Task<IEnumerable<SubscriptionItem>> GetSubscriptionsItemsAsync(string? ownerId, string? service, SubStatus? subStatus);
    }
}
