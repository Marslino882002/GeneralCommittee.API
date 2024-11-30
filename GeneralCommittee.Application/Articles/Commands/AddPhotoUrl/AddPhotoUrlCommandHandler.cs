using GeneralCommittee.Application.BunnyServices.Files.UploadFile;
using GeneralCommittee.Application.Courses.Commands.AddThumbnail;
using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Articles.Commands.AddPhotoUrl
{
    public class AddPhotoUrlCommandHandler(
    ILogger<AddPhotoUrlCommandHandler> logger,
    IArticleRepository articleRepository,
    IMediator mediator
) : IRequestHandler<AddPhotoUrlCommand, string>
    {
       
 public async Task<string> Handle(AddPhotoUrlCommand request, CancellationToken cancellationToken)
        {

            var Article
                = await articleRepository.GetArticleByIdAsync(request.ArticleId);
            var AddPhotoUrl = request.ArticleId + "-" + Guid.NewGuid() + Global.ThumbnailFileExtension;
            var uploadFileCommand = new UploadFileCommand
            {
                File = request.File,
                FileName = AddPhotoUrl,
                Directory = Global.ArticlePhotoUrlDirectory
            };
            var result = await mediator.Send(uploadFileCommand, cancellationToken);
            Article.PhotoUrl = result;
            Article.Title = AddPhotoUrl;
            await articleRepository.SaveChangesAsync();
            return result;








        }
    }
}
