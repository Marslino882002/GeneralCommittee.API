using GeneralCommittee.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Instructors.Queries.Get_Instructor_ById
{
    public class GetInstructorByIdQuery : IRequest<InstructorDto>
    {
        public int Id { get; set; }
        public string? InstructorOfName { get; set; }
    }
}
