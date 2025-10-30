using LW4_task_3.Clients;
using LW4_task_3.Interfaces;
using LW4_task_3.InterfacesRepository;
using LW4_task_3.Models.Entities;
using MongoDB.Driver;
using System.Data;

namespace LW4_task_3.Services
{
    public class PeopleService: IPeopleService
    {
        private readonly IPeopleRepository _peopleRepository;
        public PeopleService(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        public async Task CreateAsync(PeopleItem element) => await _peopleRepository.CreateAsync(element);
        

        public async Task DeleteAsync(string id)
        {
            if (!await _peopleRepository.IsExist(id))
                throw new KeyNotFoundException($"Не знайдено користувача за Id {id}");

            await _peopleRepository.DeleteAsync(id);
        }
        

        public async Task<PeopleItem> GetByIdAsync(string id)
        {

            var people =  await _peopleRepository.GetByIdAsync(id);

            if(people is null)
                throw new KeyNotFoundException($"Не знайдено користувача за Id {id}");

            return people;
        }

        public async Task<IEnumerable<PeopleItem>?> GetPeoplesItemsAsync(string? name, string? email)
        {
            var peopleItems = await _peopleRepository.GetPeoplesItemsAsync(name, email);

            if (peopleItems is null || !peopleItems.Any())
                throw new KeyNotFoundException("Не знайдено жодного користувача");

            return peopleItems;

        }

        public async Task UpdateAsync(string id, PeopleItem element)
        {
            if (!await _peopleRepository.IsExist(id))
                throw new KeyNotFoundException($"Не знайдено користувача за Id {id}");

            element.Id = id;

            await _peopleRepository.UpdateAsync(id, element);
        }
    }
}