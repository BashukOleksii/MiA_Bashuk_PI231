using LW4_task_3.Clients;
using LW4_task_3.Interface.InterfacesRepository;
using LW4_task_3.Models.Entities;
using MongoDB.Driver;

namespace LW4_task_3.Repositories
{
    public class SubRepository : ISubRepository
    {
        private readonly IMongoCollection<SubscriptionItem> _collection;

        public SubRepository()
        {
            _collection = MongoDBClient.Instance.GetCollection<SubscriptionItem>("Subscriptions");
        }
        public async Task CreateAsync(SubscriptionItem element) =>
            await _collection.InsertOneAsync(element);
        

        public async Task DeleteAsync(string id) => 
            await _collection.DeleteOneAsync(x => x.Id == id);


        public async Task<SubscriptionItem> GetByIdAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        

        public async Task<IEnumerable<SubscriptionItem>> GetSubscriptionsItemsAsync(string? ownerId, string? service, SubStatus? subStatus)
        {
            var b = Builders<SubscriptionItem>.Filter;

            var filter = b.Empty;

            if (ownerId is not null)
                filter &= b.Eq(x => x.OwnerId, ownerId);
            if (service is not null)
                filter &= b.Eq(x => x.Service, service);
            if (subStatus is not null)
                filter &= b.Eq(x => x.Status, subStatus.Value);

            var subs = await _collection.Find(filter).ToListAsync();
            
            return subs;
        }

        public async Task<bool> IsExist(string id) =>
            await _collection.Find(x => x.Id == id).AnyAsync();


        public Task UpdateAsync(string id, SubscriptionItem element) =>
            _collection.ReplaceOneAsync(x => x.Id == id, element);
        
    }
}
