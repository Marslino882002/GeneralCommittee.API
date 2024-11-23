using GeneralCommittee.Application.Common;
using GeneralCommittee.Domain.Constants;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.SystemUsers.Commands.AddRoles
{
    public class AddRolesCommand : IRequest<OperationResult<string>>
    {
        public string UserName { get; set; } = default!;
        public List<UserRoles> Roles { get; set; } = default!;
    }
}
