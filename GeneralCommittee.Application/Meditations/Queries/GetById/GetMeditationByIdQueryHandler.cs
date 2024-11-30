using AutoMapper;
using GeneralCommittee.Application.Courses.Queries.GetById;
using GeneralCommittee.Domain.Dtos;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Meditations.Queries.GetById
{
    public class GetMeditationByIdQueryHandler(
    ILogger<GetMeditationByIdQueryHandler> logger,
    IMapper mapper,
    IMeditationRepository meditationRepository
) : IRequestHandler<GetMeditationByIdQuery, MeditationDto>
    {
        public async Task<MeditationDto> Handle(GetMeditationByIdQuery request, CancellationToken cancellationToken)
        {

            var Selected_Meditation = await meditationRepository.GetMeditationsById(request.Id);

            var meditationDto = mapper.Map<MeditationDto>(Selected_Meditation); return meditationDto;


        }
    }
}
