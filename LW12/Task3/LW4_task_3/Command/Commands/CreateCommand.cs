using AutoMapper;
using LW4_task_3.Command.Interface;
using LW4_task_3.Interface.Interfaces;
using LW4_task_3.Models.Entities;
using LW4_task_3.Models.Request;
using System.Windows.Input;

namespace LW4_task_3.Command.Commands
{
    public class CreateCommand : IPeopleCommand
    {
        private readonly IPeopleService _peopleService;
        private readonly PeopleItem _people;

        public CreateCommand(IPeopleService peopleService, PeopleItem people)
        {
            _peopleService = peopleService;
            _people = people;
        }

        public async Task ExecuteAsync() =>
            await _peopleService.CreateAsync(_people);


        public async Task Undo() =>
            await _peopleService.DeleteAsync(_people.Id);
        
        
    }
}
