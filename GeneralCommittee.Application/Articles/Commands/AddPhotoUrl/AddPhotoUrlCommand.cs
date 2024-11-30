using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Articles.Commands.AddPhotoUrl
{
    public class AddPhotoUrlCommand : IRequest<string>
    {
        public int ArticleId { get; set; }
        public IFormFile File { get; set; } = default!;


    }
}
