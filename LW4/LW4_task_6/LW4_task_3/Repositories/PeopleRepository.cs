using LW4_task_3.Clients;
using LW4_task_3.Interface.InterfacesRepository;
using LW4_task_3.Models.Entities;
using MongoDB.Driver;

namespace LW4_task_3.Repositories
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly IMongoCollection<PeopleItem> _colection;

        public PeopleRepository()
        {
            _colection = MongoDBClient.Instance.GetCollection<PeopleItem>("Peoples");
        }

        public async Task CreateAsync(PeopleItem element) =>
            await _colection.InsertOneAsync(element);


        public async Task DeleteAsync(string id) =>
          await _colection.DeleteOneAsync(x => x.Id == id);

        public async Task<PeopleItem> GetByIdAsync(string id) =>
            await _colection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<PeopleItem>?> GetPeoplesItemsAsync(string? name, string? email)
        {
            var b = Builders<PeopleItem>.Filter;

            var filter = b.Empty;

            if (name is not null)
                filter &= b.Eq(x => x.Name, name);
            if (email is not null)
                filter &= b.Eq(x => x.Email, email);

            var peoples = await _colection.Find(filter).ToListAsync();

            return peoples;
        }

        public async Task<bool> IsExist(string id) =>
            await _colection.Find(x => x.Id == id).AnyAsync();


        public Task UpdateAsync(string id, PeopleItem element) =>
            _colection.ReplaceOneAsync(x => x.Id == id, element);
        
    }
}
