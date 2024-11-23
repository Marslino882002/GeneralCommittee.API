﻿using GeneralCommittee.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.SystemUsers.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<OperationResult<string>>
    {
        public string? Tenant { get; set; }
        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}