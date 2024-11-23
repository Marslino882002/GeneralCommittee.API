using GeneralCommittee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Utitlites.Jwt
{
    public interface IJwtToken
    {
        public Task<(string, string)> GetTokens(User user);
        public Task<(string, string)> RefreshTokens(string token);
    }
}
