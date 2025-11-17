using LW4_task_3.Models;

namespace LW4_task_3.Interfaces
{
    public interface IPeopleService: IService<PeopleItem>
    {
        Task<IEnumerable<PeopleItem>?> GetPeoplesItemsAsync(string? name, string? email);
    }
}
