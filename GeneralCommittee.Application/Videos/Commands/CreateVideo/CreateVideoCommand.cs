﻿using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Videos.Commands.CreateVideo
{
    public class CreateVideoCommand : IRequest<CreateVideoCommandResponse>
    {
        public string VideoName { get; set; }
        public int CourseId { get; set; }
        [MaxLength(500)]
        public string? Description { set; get; }



    }
}
