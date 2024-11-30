using AutoMapper;
using GeneralCommittee.Domain.Dtos;
using GeneralCommittee.Domain.Entities;

namespace GeneralCommittee.API.Helpers
{
    public class MappingProfiles : Profile
    {


        public MappingProfiles()
        {


            CreateMap<Article, ArticleDto>()
              .ForMember(A => A.UploadedBy, O => O.MapFrom(S => S.UploadedBy));
            CreateMap<Article, ArticleDto>()
                                    .ForMember(A => A.AuthorinDto, O => O.MapFrom(S => S.Author));


            CreateMap<Meditation, MeditationDto>()
                                   .ForMember(M=> M.UploadedBy, O => O.MapFrom(S => S.UploadedBy));



        }










    }
}
