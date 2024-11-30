using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Instructors.Command.DeleteInstructor
{
    public class DeleteInstructorCommand : IRequest
    {
        public int InstructorId { get; set; }
        public string? InstructorOfName { get; set; }

    }
}
