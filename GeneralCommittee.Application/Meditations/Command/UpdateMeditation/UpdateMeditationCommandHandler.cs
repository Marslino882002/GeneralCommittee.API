using AutoMapper;
using GeneralCommittee.Application.Articles.Commands.AddCArticle;
using GeneralCommittee.Application.Articles.Commands.UpdateArticle;
using GeneralCommittee.Application.SystemUsers;
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

namespace GeneralCommittee.Application.Meditations.Command.UpdateMeditation
{
    public class UpdateMeditationCommandHandler(
         IMeditationRepository meditationRepository,
    IConfiguration configuration,
    IMapper mapper, UserContext userContext,
    IAdminRepository adminRepository,
    ILogger<UpdateMeditationCommandHandler> logger,
    IMediator mediator) : IRequestHandler<UpdateMeditationCommand, string>
    {
        /// <summary>
        /// Handles the update of a meditation.
        /// </summary>
        /// <param name="request">The command containing updated meditation details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A message indicating the result of the update operation.</returns>
        public async Task<string> Handle(UpdateMeditationCommand request, CancellationToken cancellationToken)
        {
            var Meditation = await meditationRepository.GetMeditationsById(request.MeditationId);
            if (Meditation == null)
            {
                logger.LogWarning("Meditation with ID {MeditationId} not found.", request.MeditationId);
                return "Meditation with ID {ArticleId} not found."; // or throw an exception
            }




            // TODO: Update meditation properties
            Meditation.Title = request.Title;
            Meditation.UploadedById = request.UploadedById;
            Meditation.UploadedBy = request.UploadedBy;
            Meditation.CreatedDate = request.CreatedDate;
            Meditation.Content = request.Content;



            await meditationRepository.UpdateMeditationAsync(Meditation);
            logger.LogInformation("Article with ID {MeditationId} updated successfully.", request.MeditationId);
            return "Meditation with ID {MeditationId} updated successfully.";












        }
    }
}
