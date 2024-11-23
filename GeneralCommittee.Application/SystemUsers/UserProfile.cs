using AutoMapper;
using GeneralCommittee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.SystemUsers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterCommand, User>()
                .ForMember(u => u.TwoFactorEnabled, opt => opt.MapFrom(c => c.Active2Fa));
            CreateMap<RegisterCommand, SystemUser>();
            CreateMap<RegisterCommand, UserDto>();
        }
    }
}
