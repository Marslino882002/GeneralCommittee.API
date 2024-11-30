using AutoMapper;
using GeneralCommittee.Application.Courses.Commands.Create;
using GeneralCommittee.Application.Courses.Queries.GetById;
using GeneralCommittee.Domain.Dtos;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Articles.Queries.GetById
{
    public class GetArticleByIdQueryHandler(
    ILogger<GetArticleByIdQueryHandler> logger,
    IMapper mapper,
    IArticleRepository articleRepository
) : IRequestHandler<GetArticleByIdQuery, ArticleDto>
    {
        public async Task<ArticleDto> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var Article = await articleRepository.GetArticleByIdAsync(request.Id);
            var ArticleDto = mapper.Map<ArticleDto>(Article); return ArticleDto;





        }
    }
}
