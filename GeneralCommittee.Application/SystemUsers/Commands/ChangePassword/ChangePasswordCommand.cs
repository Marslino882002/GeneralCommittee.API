using GeneralCommittee.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.SystemUsers.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<OperationResult<string>>
    {

        public string OldPassword { get; set; } = default!;
        public string NewPassword { get; set; } = default!;





    }
}
