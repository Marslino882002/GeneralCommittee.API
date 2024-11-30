using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Instructors.Command.AddNewInstructor.AddInstructorCommand
{
    public class AddInstructorCommand : IRequest<AddInstructorCommandResponse>
    {


        [MaxLength(Global.MaxNameLength)]
        public string Name { get; set; } = default!;
        [MaxLength(Global.UrlMaxLength)]
        public IFormFile? ImageUrl { get; set; }
        public string? About { get; set; }

        public Admin AddedBy { get; set; } = default!;




    }
}
