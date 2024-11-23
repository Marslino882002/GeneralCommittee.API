using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.SystemUsers.Commands.Login
{
    public class LoginDto
    {
        bool Success { get; set; }
        public string Name { get; set; } = default!;

        public string Token { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
