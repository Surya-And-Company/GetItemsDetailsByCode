using System.ComponentModel.DataAnnotations;
using ItemsArchiveService.Model;

namespace ItemsArchiveService.CustomValidations
{
    public class IsValidRole : ValidationAttribute
    {
        public bool IsValid(string role)
        {
            if (role ==  Roles.Admin || role == Roles.User)
                return true;
            else
              return false;                                
        }
    }
}