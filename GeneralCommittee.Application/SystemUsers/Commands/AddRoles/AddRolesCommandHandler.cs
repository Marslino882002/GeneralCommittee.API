using GeneralCommittee.Application.Common;
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

namespace GeneralCommittee.Application.SystemUsers.Commands.AddRoles
{
    public class AddRolesCommandHandler(
    ILogger<AddRolesCommandHandler> logger,
    UserManager<User> userManager,
    IUserRepository systemUserRepository,
    UserContext userContext
) : IRequestHandler<AddRolesCommand, OperationResult<string>>
    {
        public async Task<OperationResult<string>> Handle(AddRolesCommand request, CancellationToken cancellationToken)
        {

            //todo check the auth
            var adminTenant = userContext.GetCurrentUser()?.Tenant;
            if (string.IsNullOrEmpty(adminTenant))
            {
                return OperationResult<string>.Failure("Unauthorized", StateCode.Unauthorized);
            }

            var changedUserRoles = await systemUserRepository.GetUserRolesAsync(request.UserName, adminTenant);
            if (changedUserRoles == -1)
            {
                return OperationResult<string>.Failure("User does not exist");
            }

            foreach (var Role in request.Roles)
            {
                var val = (int)Role;
                changedUserRoles |= (uint)(1 << val);
            }

            await systemUserRepository.SetUserRolesAsync(request.UserName, adminTenant, changedUserRoles);
            return OperationResult<string>.SuccessResult(null, "Success");


        }
    }
}
