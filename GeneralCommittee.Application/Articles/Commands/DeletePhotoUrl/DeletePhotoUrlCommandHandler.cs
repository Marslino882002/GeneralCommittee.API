using GeneralCommittee.Application.BunnyServices.Files.DeleteFile;
using GeneralCommittee.Application.Courses.Commands.DeleteThumbnail;
using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Articles.Commands.DeletePhotoUrl
{
    public class DeletePhotoUrlCommandHandler(
    IArticleRepository articleRepository,
    IMediator mediator
) : IRequestHandler<DeletePhotoUrlCommand>
    {
        public async Task Handle(DeletePhotoUrlCommand request, CancellationToken cancellationToken)
        {


            var article = await articleRepository.GetArticleByIdAsync(request.ArticleId);
            if (article is null)
            {
                return;

            }
            await mediator.Send(article, cancellationToken);
            article.PhotoUrl = null;
            await articleRepository.SaveChangesAsync();










        }
    }
}
