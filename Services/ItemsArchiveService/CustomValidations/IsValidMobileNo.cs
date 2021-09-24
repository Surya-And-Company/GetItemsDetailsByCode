using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ItemsArchiveService.CustomValidations
{
    public class IsValidMobileNo : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var regex = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            return Regex.IsMatch(value.ToString(), regex);
        }
    }
}