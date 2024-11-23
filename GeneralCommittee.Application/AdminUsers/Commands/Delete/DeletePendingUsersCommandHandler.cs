using GeneralCommittee.Application.AdminUsers.Commands.Add;
using GeneralCommittee.Application.SystemUsers;
using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Exceptions;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.AdminUsers.Commands.Delete
{/// <summary>
/// Handles the deletion of pending admin users.
/// </summary>
/// <param name="request">The command containing the list of pending users to delete.</param>
/// <param name="cancellationToken">Token to observe for cancellation requests.</param>
/// <returns>No thing</returns>
/// 
/// <remarks>
/// The following describes the logic flow of the method:
/// <list type="number">
/// <item>
/// <description>Log the retrieval of the current user from the user context.</description>
/// </item>
/// <item>
/// <description>Check if the current user is authorized: 
/// <list type="bullet">
/// <item>Fetch the current user from <c>userContext</c>.</item>
/// <item>Verify if the user has the Admin role. If not, log a warning and throw an exception.</item>
/// </list>
/// </description>
/// </item>
/// <item>
/// <description>Delete the pending users: 
/// <list type="bullet">
/// <item>Call <c>DeletePendingAsync</c> on the admin repository with the provided list of pending users.</item>
/// <item>Log success or handle any necessary exceptions as needed (additional logging can be added here).</item>
/// </list>
/// </description>
/// </item>
/// </list>
/// </remarks>
/// <exception cref="ForBidenException">
/// Thrown if the current user is not an admin.
/// </exception>
    public class DeletePendingUsersCommandHandler(
    ILogger<AddAdminCommandHandler> logger,
    IAdminRepository adminRepository,
    UserContext userContext
) : IRequestHandler<DeletePendingUsersCommand>
    {
        public async Task Handle(DeletePendingUsersCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving the current user from the context.");
            var currentUser = userContext.GetCurrentUser();

            if (currentUser == null || !currentUser.HasRole(UserRoles.Admin))
            {
                logger.LogWarning("Unauthorized attempt to delete pending users by user: {UserId}", currentUser?.Id);
                throw new ForBidenException("Don't have the permission to delete pending users.");
            }

            logger.LogInformation("Deleting pending users: {@PendingUsers} by {@}", request.PendingUsers, currentUser.Id);
            await adminRepository.DeletePendingAsync(request.PendingUsers);
            logger.LogInformation("Successfully deleted pending users: {@PendingUsers}", request.PendingUsers);
        }
    }
}
