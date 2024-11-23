using GeneralCommittee.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.SystemUsers.Commands.Login
{
    public class LoginCommand : IRequest<OperationResult<LoginDto>>
    {
        public string? Tenant { get; set; }
        public string UserIdentifier { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
