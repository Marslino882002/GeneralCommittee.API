using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.BunnyServices.VideoContent.Video.CreateVideo
{
    public class AddVideoCommand : IRequest<string?>
    {

        public string LibraryId { get; set; } = default!;
        public string CollectionId { get; set; } = default!;
        public string VideoName { get; set; } = default!;










    }
}
