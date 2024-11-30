using GeneralCommittee.Application.Articles.Commands.DeleteArticle;
using GeneralCommittee.Domain.Entities;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Meditations.Command.DeleteMeditation
{
    public class DeleteMeditationCommandHandler(IMeditationRepository _meditationRepository,
            ILogger<DeleteMeditationCommandHandler> _logger) : IRequestHandler<DeleteMeditationCommand, Unit>
    {

        /// <summary>
        /// Handles the deletion of a meditation.
        /// </summary>
        /// <param name="request">The command containing the meditation ID and Title.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task<Unit> Handle(DeleteMeditationCommand request, CancellationToken cancellationToken)
        {
            // TODO: Retrieve the meditation entity by ID
var meditation = await _meditationRepository.GetMeditationsById(request.MeditationId);

            if (meditation == null || !await _meditationRepository.IsExistByTitle(request.Title))
            {
                _logger.LogWarning("Meditation with ID: {MeditationId} and Title: {Title} not found.", request.MeditationId, request.Title);
                return Unit.Value;
            }

            // TODO: Delete the meditation
            await _meditationRepository.DeleteMeditationAsync(meditation);
            _logger.LogInformation("Meditation deleted successfully with ID: {MeditationId} and Title: {Title}", request.MeditationId, request.Title);

            return Unit.Value; // Indicate successful completion
        }
    }
}
