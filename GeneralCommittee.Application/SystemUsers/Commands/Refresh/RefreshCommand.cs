using GeneralCommittee.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.SystemUsers.Commands.Refresh
{
  public class RefreshCommand : IRequest<OperationResult<RefreshResponse>>
    {
        public string RefreshToken { get; set; } = default!;

    }
}
