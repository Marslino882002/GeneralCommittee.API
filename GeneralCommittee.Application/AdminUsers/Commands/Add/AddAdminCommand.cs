using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.AdminUsers.Commands.Add
{

    public class AddAdminCommand : IRequest
    {
        public string Email { get; set; } = default!;
    }
}
