using LW4_task_3.Models.Entities;

namespace LW4_task_3.Interface.InterfacesRepository
{
    public interface IPeopleRepository : IRepository<PeopleItem>
    {
        Task<IEnumerable<PeopleItem>?> GetPeoplesItemsAsync(string? name, string? email);
    }
}
