using GeneralCommittee.Application.Common;
using GeneralCommittee.Application.SystemUsers.Commands.AddRoles;
using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Entities;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.SystemUsers.Commands.RemoveRoles
{
    public class RemoveRolesCommandHandler(
    ILogger<AddRolesCommandHandler> logger,
    UserManager<User> userManager,
    IUserRepository systemUserRepository,
    UserContext userContext
) : IRequestHandler<RemoveRolesCommand, OperationResult<string>>
    {
        public async Task<OperationResult<string>> Handle(RemoveRolesCommand request, CancellationToken cancellationToken)
        {


            // TODO: Check if the user has admin or manager privileges
            var adminTenant = userContext.GetCurrentUser()?.Tenant;
            if (string.IsNullOrEmpty(adminTenant))
            {
                return OperationResult<string>.Failure("Unauthorized", StatusCode.Unauthorized);
            }

            var changedUserRoles = await systemUserRepository.GetUserRolesAsync(request.UserName, adminTenant);
            if (changedUserRoles == -1)
            {
                return OperationResult<string>.Failure("User does not exist");
            }

            foreach (var role in request.Roles)
            {
                changedUserRoles &= ~(1 << (int)role);
            }

            await systemUserRepository.SetUserRolesAsync(request.UserName, adminTenant, changedUserRoles);

            logger.LogInformation("Successfully removed roles from user: {UserName}", request.UserName);
            return OperationResult<string>.SuccessResult(null, "Roles removed successfully");


















        }
    }
}
