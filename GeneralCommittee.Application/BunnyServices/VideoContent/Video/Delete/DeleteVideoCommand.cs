using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.BunnyServices.VideoContent.Video.Delete
{
    public class DeleteVideoCommand : IRequest<bool>
    {
        public string VideoId { get; set; } = default!;
    }
}
