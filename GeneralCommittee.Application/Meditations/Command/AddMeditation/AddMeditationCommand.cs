using GeneralCommittee.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Meditations.Command.AddMeditation
{
    public class AddMeditationCommand : IRequest<AddMeditationCommandResponse>
    {
 public int UploadedById { get; set; } // Foreign Key property
        public Admin UploadedBy { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime CreatedDate { get; set; }=DateTime.UtcNow.Date;}}
