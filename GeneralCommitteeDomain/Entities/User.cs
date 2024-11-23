using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity;

namespace GeneralCommittee.Domain.Entities
{
public class User : IdentityUser
{
    public long Roles { get; set; }
    [MaxLength(20)]
    public string Tenant { get; set; } = default!;
}

}

