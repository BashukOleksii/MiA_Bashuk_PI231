using LW4_task_3.Interface.Interfaces;
using LW4_task_3.Interface.InterfacesRepository;
using LW4_task_3.Models.Entities;

namespace LW4_task_3.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        public UserService(IUserRepository repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task CreateAsync(UserItem element)
        {
            var user = await _repository.GetByEmailAsync(element.Email);

            if (user is not null)
                throw new ArgumentException("Пошта вже використовується");

            element.Password = _passwordHasher.Hash(element.Password);

            await _repository.CreateAsync(element);
        }
       

        public async Task DeleteAsync(string id)
        {
            if (!await _repository.IsExist(id))
                throw new KeyNotFoundException($"Не знайдено user з id: {id}");

            await _repository.DeleteAsync(id);
        }


        public async Task<List<UserItem>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if(users is null || users.Count == 0)
                throw new KeyNotFoundException($"Не знайдено users");

            return users;
        }

        public async Task<UserItem> GetByEmailAsync(string email)
        {
            var user = await _repository.GetByEmailAsync(email);

            if (user is null)
                throw new KeyNotFoundException($"Не знайдено user з вказаним email {email}");

            return user;
        }

        public async Task<UserItem> GetByIdAsync(string id)
        {
            var user = await _repository.GetByIdAsync(id);

            if(user is null)
                throw new KeyNotFoundException($"Не знайдено user з id: {id}");

            return user;
        }

        public async Task UpdateAsync(string id, UserItem element)
        {
            if(!await _repository.IsExist(id))
                throw new KeyNotFoundException($"Не знайдено user з id: {id}");

            await _repository.UpdateAsync(id, element);
        }
        
            

        
        
    }
}
