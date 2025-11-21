using AutoMapper;
using LW4_task_3.Models.Entities;
using LW4_task_3.Models.Request;
using LW4_task_3.Models.Response;

namespace LW4_task_3.Mapping
{
    public class SubProfile: Profile
    {
        public SubProfile()
        {
            CreateMap<SubscriptionRequest, SubscriptionItem>()
                .ForMember(d => d.Status, o => o.MapFrom(s => Enum.Parse<SubStatus>(s.Status,true)));

            CreateMap<SubscriptionItem, SubscriptionRequest>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

            CreateMap<SubscriptionItem, SubscriptionResponse>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));
        }
    }
}
