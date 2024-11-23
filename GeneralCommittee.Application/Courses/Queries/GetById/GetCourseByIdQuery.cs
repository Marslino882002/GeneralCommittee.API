using GeneralCommittee.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Courses.Queries.GetById
{
    public class GetCourseByIdQuery : IRequest<CourseDto>
    {
        public int Id { get; set; }
    }
}
