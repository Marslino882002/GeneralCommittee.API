using AutoMapper;
using GeneralCommittee.Application.Articles.Commands.AddCArticle;
using GeneralCommittee.Application.SystemUsers;
using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Entities;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Articles.Commands.UpdateArticle
{
    public class UpdateArticleCommandHandler(
        IArticleRepository ArticleRepositor,
    IConfiguration configuration,
    IMapper mapper, UserContext userContext,
    IAdminRepository adminRepository,
    ILogger<AddArticleCommandHandler> logger,
    IMediator mediator) : IRequestHandler<UpdateArticleCommand , string>
    {
        
async Task<string> IRequestHandler<UpdateArticleCommand, string>.Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {


            //TODO : This part checks if the current user is authenticated and has the 'Admin' role.
            //If not, it throws an UnauthorizedAccessException.
            var currentUser = userContext.GetCurrentUser();
            if (currentUser == null || !currentUser.HasRole(UserRoles.Admin))
                throw new UnauthorizedAccessException();
            var admin = await adminRepository.GetAdminByIdentityAsync(currentUser.Id);

  var article = await ArticleRepositor.GetArticleByIdAsync(request.ArticleId);
            if (article == null)
            {
            logger.LogWarning("Article with ID {ArticleId} not found.", request.ArticleId);
                return "Article with ID {ArticleId} not found."; // or throw an exception
            }
            //TODO: Update the Article entity from the command
            article.ArticleId = request.ArticleId;
            article.Author = request.Author;
            article.AuthorId = request.AuthorId;
            article.UploadedById = request.UploadedById;
            article.Content = request.Content;
            article.PhotoUrl = request.PhotoUrl;
            article.Title = request.Title;
            article.CreatedDate = request.CreatedDate;
            article.UploadedBy = request.UploadedBy;



            await ArticleRepositor.SaveChangesAsync();
            logger.LogInformation("Article with ID {ArticleId} updated successfully.", request.ArticleId);
            return "Article with ID {ArticleId} updated successfully.";







        }
    }
}
