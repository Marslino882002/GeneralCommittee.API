using GeneralCommittee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Repositories
{
    public interface IUserRepository
    {


        public Task<bool> RegisterUser(User user, string password, SystemUser userToRegister);
        public Task<Guid?> GetUserTokenCodeAsync(User user);
        public Task<Guid> ChangePasswordAsync(User user);

        public Task<long> GetUserRolesAsync(string username, string tenant);
        public Task SetUserRolesAsync(string username, string tenant, long roles);
        public Task<User?> GetUserByUserNameAsync(string userName, string tenant);
        public Task<User?> GetUserByEmailAsync(string email, string tenant);
        public Task<User?> GetUserByPhoneNumberAsync(string phoneNumber, string tenant);





    }
}
