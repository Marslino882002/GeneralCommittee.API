using AutoMapper;
using GeneralCommittee.Application.Articles.Commands.AddCArticle;
using GeneralCommittee.Application.BunnyServices.Files.DeleteFile;
using GeneralCommittee.Application.SystemUsers;
using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Entities;
using GeneralCommittee.Domain.Exceptions;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Articles.Commands.DeleteArticle
{
    public class DeleteArticleCommandHandler(
 IConfiguration configuration,
     UserContext userContext,
    IAdminRepository adminRepository,
    IArticleRepository articleRepositor,
        IMediator mediator,
        ILogger<DeleteArticleCommandHandler> logger

        ) : IRequestHandler<DeleteArticleCommand>
    {

        public async Task Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {



            // Check if the current user is authenticated and has the 'Admin' role.
            var currentUser = userContext.GetCurrentUser();
            if (currentUser == null || !currentUser.HasRole(UserRoles.Admin))
            {
                throw new UnauthorizedAccessException();
            }var admin = await adminRepository.GetAdminByIdentityAsync(currentUser.Id);

           // TODO:Retrieve the article by ID

            var Article = await articleRepositor.GetArticleByIdAsync(request.ArticleId);
            if (Article is null || !await articleRepositor.IsExistByTitle(request.title))
            {
                logger.LogWarning("Article with ID {ArticleId} not found.", request.ArticleId);
                throw new ResourceNotFound(nameof(Article), request.ArticleId.ToString());

            } // Delete the article
            await articleRepositor.DeleteArticleAsync(Article);
            await articleRepositor.SaveChangesAsync();
         logger.LogInformation("Article Deleted successfully with ID: {ArticleId}", request.ArticleId);
          //  return Unit.Value; // TODO :successful completion







        }

       
    }
}
