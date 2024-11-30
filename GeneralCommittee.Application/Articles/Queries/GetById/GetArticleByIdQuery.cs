using GeneralCommittee.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Articles.Queries.GetById
{
    public class GetArticleByIdQuery : IRequest<ArticleDto>
    {


        public int Id { get; set; }



    }
}
