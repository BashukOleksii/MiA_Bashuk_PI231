using AutoMapper;
using LW4_task_3.Models.Entities;
using LW4_task_3.Models.Request;
using LW4_task_3.Models.Response;
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace LW4_task_3.Mapping
{
    public class MessageProfile: Profile
    {
        public MessageProfile()
        {
            CreateMap<MessageRequest, MessageItem>().ReverseMap();
            CreateMap<MessageItem, MessageResponse>();
        }
    }
}
