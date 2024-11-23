using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Courses.Commands.AddThumbnail
{
    public class AddCourseThumbnailCommand : IRequest<string>
    {

        public int CourseId { get; set; }
        public IFormFile File { get; set; } = default!;


    }
}
