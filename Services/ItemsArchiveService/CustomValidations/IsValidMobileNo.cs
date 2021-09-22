using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ItemsArchiveService.CustomValidations
{
    public class IsValidMobileNo : ValidationAttribute
    {
        public bool IsValid(string mobileNo)
        {
            var regex = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            return Regex.IsMatch(mobileNo, regex);
        }
    }
}