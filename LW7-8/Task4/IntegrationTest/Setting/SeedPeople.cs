using LW4_task_3.Models.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Setting
{
    public class SeedPeople
    {
        private readonly IMongoDatabase _db;

        public SeedPeople(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task SeedPeoplesAsync()
        {
            var collection = _db.GetCollection<PeopleItem>("Peoples");

            await collection.DeleteManyAsync(FilterDefinition<PeopleItem>.Empty);

            var peoples = new List<PeopleItem>()
            {
                    new PeopleItem() {Id = "68f5e054f21b02f6aece46ee", Name = "Kate", Email = "Kate@Email.com" },
                    new PeopleItem() {Id = "68f5e06bf21b02f6aece46ef", Name = "John", Email = "John@Email.com" },
                    new PeopleItem() {Id = "68ff7822a866d7d74955a11c", Name = "Jane", Email = "Jane@Email.com" },
                    new PeopleItem() {Id = "690113f4b26a5f31097a0fad", Name = "Steve", Email = "Steve@Email.com" },
                    new PeopleItem() {Id = "69039a91e402e8cfbe2a51e1", Name = "Alex", Email = "Alex@Email.com" }
            };

            await collection.InsertManyAsync(peoples);
        }
    }
}
