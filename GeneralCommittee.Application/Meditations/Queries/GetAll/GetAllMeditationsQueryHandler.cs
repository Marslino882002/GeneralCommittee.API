using AutoMapper;
using GeneralCommittee.Application.Common;
using GeneralCommittee.Application.Courses.Queries.GetAll;
using GeneralCommittee.Domain.Dtos;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Meditations.Queries.GetAll
{
    public class GetAllMeditationsQueryHandler(
    ILogger<GetAllMeditationsQueryHandler> logger,
    IMapper mapper,
    IMeditationRepository meditationRepository
    ) : IRequestHandler<GetAllMeditationsQuery, PageResult<MeditationDto>>
    {
        public async Task<PageResult<MeditationDto>> Handle(GetAllMeditationsQuery request, CancellationToken cancellationToken)
        {



            // TODO: add auth
            logger.LogInformation("Retrieving all Meditations with search text: {SearchText}, page number: {PageNumber}, page size: {PageSize}", request.SearchText, request.PageNumber, request.PageSize);

            // Retrieve all Meditations from the repository
            var pendingUsers = await meditationRepository.GetAllMeditationsAsync(request.SearchText, request.PageNumber, request.PageSize , request.sortBy);

            // Log the number of Meditations retrieved
            logger.LogInformation("Retrieved {Meditation} Meditations.", pendingUsers.Item1);

            // Map the retrieved courses to DTOs
            var MeditationDto = mapper.Map<IEnumerable<MeditationDto>>(pendingUsers.Item2);

            // Create the page result
            var count = pendingUsers.Item1;
            var ret = new PageResult<MeditationDto>(MeditationDto, count, request.PageSize, request.PageNumber , request.sortBy);

            return ret;

















        }
    }
}
