using AutoMapper;
using GeneralCommittee.Application.Courses.Commands.Create;
using GeneralCommittee.Domain.Dtos;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Courses.Queries.GetById
{
    public class GetCourseByIdQueryHandler(
    ILogger<CreateCourseCommandHandler> logger,
    IMapper mapper,
    ICourseRepository courseRepository
) : IRequestHandler<GetCourseByIdQuery, CourseDto>
    {


        public async Task<CourseDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.GetByIdAsync(request.Id);
            return course;
        }







    }
}
