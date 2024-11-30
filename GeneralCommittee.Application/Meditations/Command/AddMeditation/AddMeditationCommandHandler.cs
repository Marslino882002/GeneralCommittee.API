using AutoMapper;
using GeneralCommittee.Application.Articles.Commands.AddCArticle;
using GeneralCommittee.Application.SystemUsers;
using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Entities;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Meditations.Command.AddMeditation
{
    public class AddMeditationCommandHandler(
    IMeditationRepository meditationRepository,
    IConfiguration configuration,
    IMapper mapper, UserContext userContext,
    IAdminRepository adminRepository,
    ILogger<AddMeditationCommandHandler> logger,
    IMediator mediator) : IRequestHandler<AddMeditationCommand, AddMeditationCommandResponse>
    {

        /// <summary>
        /// Handles the addition of a new meditation.
        /// </summary>
        /// <param name="request">The command containing meditation details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A response containing the ID of the newly added meditation.</returns>
        async Task<AddMeditationCommandResponse> IRequestHandler<AddMeditationCommand, AddMeditationCommandResponse>.Handle(AddMeditationCommand request, CancellationToken cancellationToken)
        {
            //TODO : Check if the current user is authorized
            var currentUser = userContext.GetCurrentUser();
            if (currentUser == null || !currentUser.HasRole(UserRoles.Admin))
                throw new UnauthorizedAccessException();
            var admin = await adminRepository.GetAdminByIdentityAsync(currentUser.Id);

            //TODO: Create the Meditation entity from the command
            var meditation = new Meditation
            {Title = request.Title,
                Content = request.Content,
            UploadedBy = request.UploadedBy,
                UploadedById = request.UploadedById,
            };



            //TODO : Validation: The code checks
            //if the Title and Content properties are not null
            //or whitespace, throwing an exception if they are invalid.
            var result = await mediator.Send(meditation, cancellationToken);
            if (string.IsNullOrEmpty(request.Content) ||
           string.IsNullOrEmpty(request.Title) )
                logger.LogError("One or more Of Fields in Required Data  is Empty");




            //TODO: Save The article In DB using the repository
            var New_Meditation = await meditationRepository.AddMeditationAsync(meditation);
            logger.LogInformation("meditation created successfully with This Information: {ArticleId}", result);

            var MeditationMapper = mapper.Map<Meditation>(request);
            var ret = new AddMeditationCommandResponse
            {
                MeditationId = New_Meditation
            };
            return ret;








        }
    }
}
