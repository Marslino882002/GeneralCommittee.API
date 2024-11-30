using AutoMapper;
using GeneralCommittee.Application.Articles.Queries.GetById;
using GeneralCommittee.Domain.Dtos;
using GeneralCommittee.Domain.Exceptions;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Instructors.Queries.Get_Instructor_ById
{
    internal class GetInstructorByIdQueryHandler(
    ILogger<GetInstructorByIdQueryHandler> logger,
    IMapper mapper,
    IInstructorRepository InstructorRepository
) : IRequestHandler<GetInstructorByIdQuery, InstructorDto>
    {
        public async Task<InstructorDto> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {

            try
            { // Log the incoming request
                logger.LogInformation("Retrieving instructor with ID: {Id} or Name: {Name}", request.Id, request.InstructorOfName);
                // Retrieve the instructor by ID or name
                var selectedInstructor = await InstructorRepository.GetInstructorByIdOrName(request.Id, request.InstructorOfName);
                // Check if the instructor exists
                if (selectedInstructor == null)
                {
                    logger.LogWarning("Instructor with ID {Id} or Name {Name} not found.", request.Id, request.InstructorOfName);
                    throw new ResourceNotFound(nameof(Instructor), request.Id.ToString());
                }
                // Map the instructor to the DTO
                var instructorDto = mapper.Map<InstructorDto>(selectedInstructor);
                // Log the successful retrieval
                logger.LogInformation("Successfully retrieved instructor with ID: {Id}", request.Id); return instructorDto;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving the instructor with ID: {Id}", request.Id);
                throw;


            }
        }
    }
}
