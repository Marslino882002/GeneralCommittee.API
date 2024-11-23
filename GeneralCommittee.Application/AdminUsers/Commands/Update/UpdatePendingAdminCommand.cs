using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.AdminUsers.Commands.Update
{
    public class UpdatePendingAdminCommand : IRequest
    {

        public string OldEmail { get; set; } = default!;
        public string NewEmail { get; set; } = default!;




    }
}
