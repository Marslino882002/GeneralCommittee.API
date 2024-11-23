using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.BunnyServices.Files.UploadFile
{
    public class UploadFileCommand : IRequest<string>
    {
        public IFormFile File { get; set; } = default!;
        public string Directory { get; set; } = default!;
        public string FileName { get; set; } = default!;
    }
}
