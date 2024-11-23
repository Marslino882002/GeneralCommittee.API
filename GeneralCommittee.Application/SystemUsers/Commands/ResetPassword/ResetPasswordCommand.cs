using GeneralCommittee.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.SystemUsers.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<OperationResult<string>>
    {

        public string? Tenant { get; set; }
        public string Email { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
        public string ResetCode { get; set; } = default!;







    }
}
