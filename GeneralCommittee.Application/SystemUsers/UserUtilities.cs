using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.SystemUsers
{
    public static class UserUtilities
    {
        public static bool IsValidEmail(this string input)
        {
            var emailChecker = new EmailAddressAttribute();
            return emailChecker.IsValid(input);
        }

        public static bool IsValidPhoneNumber(this string input)
        {
            var phoneNumberPattern = @"^\d{7,15}$";
            return Regex.IsMatch(input, phoneNumberPattern);
        }
    }
}
