using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using webf.App_GlobalResources;

namespace webf.Models
{

    public class ChangePasswordModel
    {
        [Required(ErrorMessageResourceName = "ErrorRequiredString",
                  ErrorMessageResourceType = typeof(ErrorStrings))]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequiredString",
                  ErrorMessageResourceType = typeof(ErrorStrings))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required(ErrorMessageResourceName = "ErrorRequiredString",
                  ErrorMessageResourceType = typeof(ErrorStrings))]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequiredString",
                  ErrorMessageResourceType = typeof(ErrorStrings))]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessageResourceName = "ErrorRequiredString", 
                  ErrorMessageResourceType = typeof(ErrorStrings))]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequiredString",
                  ErrorMessageResourceType = typeof(ErrorStrings))]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequiredString",
                  ErrorMessageResourceType = typeof(ErrorStrings))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторить пароль")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
