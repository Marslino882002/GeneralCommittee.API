using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Infrastructure.Validators
{
    public class RegisterUserUserValidator<TUser> : UserValidator<TUser> where TUser : IdentityUser
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            var result = await base.ValidateAsync(manager, user);
            var errors = new List<IdentityError>();
            foreach (var error in result.Errors)
            {
                var er = new IdentityError();
                er.Code = "DuplicateUserName";
                if (error.Code == er.Code)
                    continue;
                errors.Add(error);
            }

            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }
    }
}
