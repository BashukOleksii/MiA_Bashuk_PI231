using LW4_task_3.Clients;
using LW4_task_3.Interface.InterfacesRepository;
using LW4_task_3.Models.Entities;
using MongoDB.Driver;

namespace LW4_task_3.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoCollection<MessageItem> _collection;

        public MessageRepository()
        {
            _collection = MongoDBClient.Instance.GetCollection<MessageItem>("Messages");
        }
        public async Task CreateAsync(MessageItem element) =>
            await _collection.InsertOneAsync(element);
        

        public async Task DeleteAsync(string id) => 
            await _collection.DeleteOneAsync(x => x.Id == id);


        public async Task<MessageItem> GetByIdAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        

        public async Task<IEnumerable<MessageItem>> GetMessageItemsAsync(string? title, string? ownerId, string? subId)
        {
            var b = Builders<MessageItem>.Filter;
            var filter = b.Empty;

            if (title is not null)
                filter &= b.Eq(x => x.Title, title);
            if (ownerId is not null)
                filter &= b.Eq(x => x.OwnerId, ownerId);
            if (subId is not null)
                filter &= b.Eq(x => x.SubId, subId);

            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<bool> IsExist(string id) =>
            await _collection.Find(x => x.Id == id).AnyAsync();


        public async Task UpdateAsync(string id, MessageItem element) =>
            await _collection.ReplaceOneAsync(x => x.Id == id, element);
        
    }
}
