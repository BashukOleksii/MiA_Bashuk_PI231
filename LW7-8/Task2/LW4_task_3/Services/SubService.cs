using LW4_task_3.Clients;
using LW4_task_3.Interface.Interfaces;
using LW4_task_3.Interface.InterfacesRepository;
using LW4_task_3.Models.Entities;
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
            if (!await _subRepository.IsExist(id))
                throw new KeyNotFoundException($"Не знайдено підписки за вказаним Id {id}");

            await _subRepository.DeleteAsync(id);
        }

        public async Task<SubscriptionItem> GetByIdAsync(string id)
        {
            var sub = await _subRepository.GetByIdAsync(id);

            if (sub is null)
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

            await _subRepository.UpdateAsync(id, element);
        }

        public async Task ValidOwner(string ownerID)
        {
            if (!await _peopleRepository.IsExist(ownerID))
                throw new ArgumentException($"Не знайдено власника з id {ownerID} для вказаної підписки");
        }




        public async Task<int> CountAllSub()
        {
            IEnumerable<SubscriptionItem> items;

            items = await _subRepository.GetSubscriptionsItemsAsync(null, null, null);
            return items.Count();
        }

        public async Task<double> PercentService(string ServiceName)
        {
            if (string.IsNullOrWhiteSpace(ServiceName))
                throw new ArgumentNullException("Передано пустий параметр");

            IEnumerable<SubscriptionItem> items;


            items = await _subRepository.GetSubscriptionsItemsAsync(null, null, null);
            int countService = 0;

            if (!items.Any())
                return 0;

            foreach (var sub in items)
                if (sub.Service.ToLower() == ServiceName.ToLower())
                    countService++;

            return ((double)countService / items.Count()) * 100;

        }




        public async Task<SubStatus> PopularStatus()
        {
            Dictionary<SubStatus, int> statuses = new Dictionary<SubStatus, int>();


            IEnumerable<SubscriptionItem> items = await _subRepository.GetSubscriptionsItemsAsync(null, null, null);

            if (!items.Any())
                return SubStatus.None;

            foreach (var sub in items)
            {
                if (statuses.ContainsKey(sub.Status))
                    statuses[sub.Status]++;
                else
                    statuses.Add(sub.Status, 1);
            }

            return statuses.OrderByDescending(v => v.Value).First().Key;



        }

        public async Task<int> CountSubByStatus(SubStatus status)
        {
            if (status == SubStatus.None || status == null)
                throw new ArgumentNullException("Невірний статус");

            IEnumerable<SubscriptionItem> items;

            items = await _subRepository.GetSubscriptionsItemsAsync(null, null, status);

            if (!items.Any())
                return 0;

            return items.Count();

        }

        public async Task<string[]> Top3Service()
        {
            Dictionary<string, int> services = new Dictionary<string, int>();


            IEnumerable<SubscriptionItem> items = await _subRepository.GetSubscriptionsItemsAsync(null, null, null);

            if (!items.Any())
                return Array.Empty<string>();

            foreach (var sub in items)
            {
                if (services.ContainsKey(sub.Service))
                    services[sub.Service]++;
                else
                    services.Add(sub.Service, 1);
            }

            return services.OrderByDescending(s => s.Value).Take(3).Select(s => s.Key).ToArray();

        }



    }
}
