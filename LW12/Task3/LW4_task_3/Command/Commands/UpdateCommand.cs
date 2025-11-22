using LW4_task_3.Command.Interface;
using LW4_task_3.Interface.Interfaces;
using LW4_task_3.Models.Entities;

namespace LW4_task_3.Command.Commands
{
    public class UpdateCommand : IPeopleCommand
    {
        private readonly IPeopleService _service;
        private readonly PeopleItem _updatedPeople;
        private readonly string _id;

        private PeopleItem _startPeople;

        public UpdateCommand(IPeopleService service,PeopleItem updatesPeople, string id)
        {
            _service = service;
            _updatedPeople = updatesPeople;
            _id = id;
        }

        public async Task ExecuteAsync()
        {
            _startPeople = await _service.GetByIdAsync(_id);

            await _service.UpdateAsync(_id, _updatedPeople);
        }

        public async Task Undo() =>
            await _service.UpdateAsync(_id, _startPeople);
        
    }
}
