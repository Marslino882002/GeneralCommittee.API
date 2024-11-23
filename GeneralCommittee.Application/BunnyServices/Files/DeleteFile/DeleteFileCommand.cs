using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.BunnyServices.Files.DeleteFile
{
    public class DeleteFileCommand : IRequest
    {


        public string Directory { get; set; } = default!;
        public string FileName { get; set; } = default!;







    }
}
