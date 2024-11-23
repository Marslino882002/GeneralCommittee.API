using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Videos.Commands.ConfirmUpload
{
    public class ConfirmUploadCommand : IRequest
    {
        public string videoId { get; set; }
        public bool Confirmed { get; set; }
    }
}
