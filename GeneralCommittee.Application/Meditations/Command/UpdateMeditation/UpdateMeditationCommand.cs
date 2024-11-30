using GeneralCommittee.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Meditations.Command.UpdateMeditation
{
    public class UpdateMeditationCommand  : IRequest<string>
    {
        public int MeditationId { get; set; }
        public string Title { get; set; } = default!;
        public int UploadedById { get; set; } // Foreign Key property
        public Admin UploadedBy { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; } = default!;
    }
}
