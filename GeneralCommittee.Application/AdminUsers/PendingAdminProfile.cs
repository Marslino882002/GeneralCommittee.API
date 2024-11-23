using AutoMapper;
using GeneralCommittee.Application.AdminUsers.Commands.Register;
using GeneralCommittee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.AdminUsers
{
    public class PendingAdminProfile : Profile
    {

        public PendingAdminProfile()
        {
            CreateMap<PendingUsersDto, PendingAdmins>().ReverseMap();
            CreateMap<RegisterAdminCommand, User>()
                .ForMember(u => u.TwoFactorEnabled, opt => opt.MapFrom(c => c.Active2Fa)).ReverseMap();
        }





    }
}
