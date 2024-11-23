using GeneralCommittee.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.BunnyServices.VideoContent.Video.Get
{
public class GetVideoInfoCommand:IRequest<VideoInfoDto?>
    
    {

        public string LibraryId { get; set; } = default!;
        public string VideoId { get; set; } = default!;


    }
}
