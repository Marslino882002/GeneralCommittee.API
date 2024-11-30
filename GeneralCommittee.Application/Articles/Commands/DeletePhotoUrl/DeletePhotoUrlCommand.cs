using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Articles.Commands.DeletePhotoUrl
{
    public class DeletePhotoUrlCommand : IRequest
    {


        public int ArticleId { get; set; }
    }
}
