using System;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using webf.App_GlobalResources;

namespace webf.Models.EntityModel
{
    [MetadataType(typeof(UserProfileMetadata)) ]
    public partial class UserProfile
    {
        public string FullName
        {
            get { return LastName + " " + FirstName + " " + ParentName; }
        }
    }

    public class UserProfileMetadata
    {
        [DisplayName("Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorStrings),
                    ErrorMessageResourceName = "ErrorRequiredString")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorStrings),
                    ErrorMessageResourceName = "ErrorRequiredString")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string ParentName { get; set; }

        [Required(  ErrorMessageResourceType = typeof(ErrorStrings),
                    ErrorMessageResourceName = "ErrorRequiredString")]
        [Display(Name = "Телефон(мобильный)")]
        public string PhoneNumMobile { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorStrings),
                    ErrorMessageResourceName = "ErrorRequiredString")]
        [Display(Name = "Телефон(городской)")]
        public string PhoneNumWorkGTS { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorStrings),
                    ErrorMessageResourceName = "ErrorRequiredString")]
        [Display(Name = "Телефон(ОС)")]
        public string PhoneNumWorkOS { get; set; }



    }

    
}