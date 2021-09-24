using System;
using System.ComponentModel.DataAnnotations;
using ItemsArchiveService.Model;
using ItemsArchiveService.Utility;

namespace ItemsArchiveService.CustomValidations
{
    public class IsValidRole : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (Enum.TryParse(value.ToString(), out Role role))
            {
                if (role == Role.Admin || role == Role.User)
                    return true;
            }
            return false;

        }
    }
}