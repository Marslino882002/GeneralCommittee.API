using AutoMapper;
using GeneralCommittee.Application.Articles.Queries.GetAllArticles;
using GeneralCommittee.Application.Common;
using GeneralCommittee.Domain.Dtos;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Instructors.Queries.GetAll_Instructors
{
    public class GetAllInstructorsQueryHandler(
ILogger<GetAllInstructorsQueryHandler> logger,
    IMapper mapper,
    IInstructorRepository instructorRepository

        )
        : IRequestHandler<GetAllInstructorsQuery, PageResult<InstructorDto>>
    {
        public async Task<PageResult<InstructorDto>> Handle(GetAllInstructorsQuery request, CancellationToken cancellationToken)
        {



            // TODO: add auth
            logger.LogInformation("Retrieving all Instructors with search text: {SearchText}, page number: {PageNumber}, page size: {PageSize}", request.SearchText, request.PageNumber, request.PageSize );


            // Retrieve all Articles from the repository
            var Instructor = await instructorRepository.GetAllInstructors(request.SearchText, request.PageNumber, request.PageSize);

            // Log the number of Articles retrieved
            logger.LogInformation("Retrieved {Count} Instructors.", Instructor.Item1);

            // Map the retrieved Articles to DTOs
            var instructorDtos = mapper.Map<IEnumerable<InstructorDto>>(Instructor.Item2);

            // Create the page result
            var count = Instructor.Item1;
            var ret = new PageResult<InstructorDto>(instructorDtos, count, request.PageSize, request.PageNumber);

            return ret;










        }
    }
}
