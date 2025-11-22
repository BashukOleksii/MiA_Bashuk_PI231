using LW4_task_3.Clients;
using LW4_task_3.Interface.InterfacesRepository;
using LW4_task_3.Models.Entities;
using MongoDB.Driver;

namespace LW4_task_3.Repositories
{
    public class UserRepository : IUserRepository
    {
        IMongoCollection<UserItem> _users;

        public UserRepository()
        {
            _users = MongoDBClient.Instance.GetCollection<UserItem>("Users");
        }

        public async Task CreateAsync(UserItem element) =>
            await _users.InsertOneAsync(element);
       

        public async Task DeleteAsync(string id) => 
            await _users.DeleteOneAsync(x => x.Id == id);
       

        public async Task<List<UserItem>> GetAllAsync() =>
            await _users.Find(x => true).ToListAsync();


        public async Task<UserItem> GetByEmailAsync(string email) =>
            await _users.Find(x => x.Email == email).FirstOrDefaultAsync();

        public async Task<UserItem> GetByIdAsync(string id) =>
            await _users.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<bool> IsExist(string id) =>
            await _users.Find(x => x.Id == id).AnyAsync();

        public async Task UpdateAsync(string id, UserItem element) =>
            await _users.ReplaceOneAsync(x => x.Id == id, element);
        
    }
}
