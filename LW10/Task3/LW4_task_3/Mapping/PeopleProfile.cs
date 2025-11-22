using AutoMapper;
using LW4_task_3.Models.Entities;
using LW4_task_3.Models.Request;
using LW4_task_3.Models.Response;

namespace LW4_task_3.Mapping
{
    public class PeopleProfile: Profile
    {
        public PeopleProfile()
        {
            CreateMap<PeopleRequest, PeopleItem>().ReverseMap();
            CreateMap<PeopleItem, PeopleResponse>();
        }

    }
}
