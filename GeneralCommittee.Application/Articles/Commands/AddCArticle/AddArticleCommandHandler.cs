using AutoMapper;
using GeneralCommittee.Application.Articles.Commands.AddArticle;
using GeneralCommittee.Application.BunnyServices.Files.UploadFile;
using GeneralCommittee.Application.Common;
using GeneralCommittee.Application.Courses.Commands.AddThumbnail;
using GeneralCommittee.Application.Courses.Commands.Create;
using GeneralCommittee.Application.SystemUsers;
using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Dtos;
using GeneralCommittee.Domain.Entities;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Articles.Commands.AddCArticle
{
    public class AddArticleCommandHandler( 
    IArticleRepository ArticleRepositor ,
    IConfiguration configuration ,
    IMapper mapper, UserContext userContext,
    IAdminRepository adminRepository ,
    ILogger<AddArticleCommandHandler> logger ,
    IMediator mediator) : IRequestHandler<AddArticleCommand, AddArticleCommandResponse>
    {
 async Task<AddArticleCommandResponse> IRequestHandler<AddArticleCommand, AddArticleCommandResponse>.Handle(AddArticleCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();
            if (currentUser == null || !currentUser.HasRole(UserRoles.Admin))
            logger.LogError("Unauthorized access attempt by user.");
            throw new UnauthorizedAccessException();
 var admin = await adminRepository.GetAdminByIdentityAsync(currentUser.Id);
            //TODO : Check if the required fields are not null or empty
            if (string.IsNullOrEmpty(request.Content) || string.IsNullOrEmpty(request.Title)) 
      { logger.LogError("One or more required fields are empty."); 
                throw new ArgumentException("All fields must be provided."); }
            //TODO: Create the Article entity from the command
            var article = new Article
            {
                AuthorId = request.AuthorId,
                UploadedById = request.UploadedById,
                Content = request.Content,
                Title = request.Title,
                PhotoUrl = request.photo.ToString(),
                CreatedDate = DateTime.UtcNow // Set the created date
            };

            //TODO : Validation: The code checks
            //if the Title and Content properties are not null
            //or whitespace, throwing an exception if they are invalid.
            var result = await mediator.Send(article, cancellationToken);
            if (string.IsNullOrEmpty(request.Content) ||
           string.IsNullOrEmpty(request.Title))
                logger.LogError("One or more Of Fields in Required Data  is Empty");
            throw new ArgumentException("All fields must be provided.");

            //TODO: Save The article In DB using the repository
            var id = await ArticleRepositor.CreateAsync(article);
    logger.LogInformation("Article created successfully with This Information: {ArticleId}", result);

 var ArticleMapper = mapper.Map<Article>(request);
            var ret = new AddArticleCommandResponse
            {
                ArticleId = id
            };
            return ret;

        }
    }
}
