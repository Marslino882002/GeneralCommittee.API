using GeneralCommittee.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Courses.Commands.Create
{
    public class CreateCourseCommand : IRequest<CreateCourseCommandResponse>
    {
        [MaxLength(Global.TitleMaxLength)] public string Name { set; get; } = default!;
        public IFormFile? Thumbnail { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = String.Empty;
        public bool IsFree { get; set; } = false;
        public int InstructorId { get; set; } = default!;
    }
}
