using AutoMapper;
using LW4_task_3.Models.Entities;
using LW4_task_3.Models.Request;

namespace LW4_task_3.Mapping
{
    public class UserProfile:Profile
    {
        public UserProfile() 
        {
            CreateMap<UserRegistration, UserItem>();
        }
    }
}
