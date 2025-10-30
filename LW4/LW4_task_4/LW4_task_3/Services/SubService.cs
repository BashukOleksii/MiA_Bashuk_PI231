using LW4_task_3.Clients;
using LW4_task_3.Interfaces;
using LW4_task_3.InterfacesRepository;
using LW4_task_3.Models;
using MongoDB.Driver;
using System.Xml.Linq;

namespace LW4_task_3.Services
{
    public class SubService : ISubService
    {
        private readonly ISubRepository _subRepository;
        private readonly IPeopleRepository _peopleRepository;

        public SubService(ISubRepository subRepository, IPeopleRepository peopleRepository)
        {
            _subRepository = subRepository;
            _peopleRepository = peopleRepository;
        }

        public async Task CreateAsync(SubscriptionItem element)
        {
            await ValidOwner(element.OwnerId);
            await _subRepository.CreateAsync(element);
        }
        
        public async Task DeleteAsync(string id)
        {
            if(!await _subRepository.IsExist(id))
                throw new KeyNotFoundException($"Не знайдено підписки за вказаним Id {id}");

            await _subRepository.DeleteAsync(id);
        }

        public async Task<SubscriptionItem> GetByIdAsync(string id)
        {
            var sub = await _subRepository.GetByIdAsync(id);

            if(sub is null)
                throw new KeyNotFoundException($"Не знайдено підписки за вказаним Id {id}");

            return sub;
        }

        public async Task<IEnumerable<SubscriptionItem>> GetSubscriptionsItemsAsync(string? ownerId, string? service, SubStatus? subStatus)
        {
            var subs = await _subRepository.GetSubscriptionsItemsAsync(ownerId, service, subStatus);

            if (subs is null || !subs.Any())
                throw new KeyNotFoundException("Не знайдено жодної підписки");

            return subs;
        }

        public async Task UpdateAsync(string id, SubscriptionItem element)
        {
            if (!await _subRepository.IsExist(id))
                throw new KeyNotFoundException($"Не знайдено підписки за вказаним Id {id}");

            await ValidOwner(element.OwnerId);

            element.Id = id;

            await _subRepository.UpdateAsync(id,element);
        }

        public async Task ValidOwner(string ownerID)
        {
            if(!await _peopleRepository.IsExist(ownerID))
                throw new ArgumentException($"Не знайдено власника з id {ownerID} для вказаної підписки");
        }
               
        
    }
}
