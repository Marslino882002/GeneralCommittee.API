using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.AdminUsers.Commands.Delete
{
    public class DeletePendingUsersCommand : IRequest
    {
        public List<string> PendingUsers { get; set; } = new();

    }
}
