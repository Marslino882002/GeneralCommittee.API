using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.BunnyServices.VideoContent.Video.Upload
{
    public class UploadVideoCommand : IRequest<bool>
    {
        public string VideoId { get; set; } = default!;
        public string LibraryName { get; set; } = default!;
        public string FilePath { get; set; } = default!;
    }
}
