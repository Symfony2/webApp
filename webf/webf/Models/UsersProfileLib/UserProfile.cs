using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webf.App_GlobalResources;
using webf.SvcDependencies;

namespace webf.Models.EntityModel
{
    
    public class RegForm: Ancestor    //: DefaultModelBinder
    {
        public  RegForm()
        {
            
        }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Отчество")]
        public string ParentName { get; set; }
        
        [Display(Name = "Дата рождения")]
        [RegularExpression(@"\d{1,2}\.\d{1,2}\.\d{4}", 
            ErrorMessageResourceName = "DateFormatIncorrect", 
            ErrorMessageResourceType = typeof(ErrorStrings))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Должность")]
        public string Post { get; set; }
        
        [Display(Name = "Телефон(домащний)")]
        public string PhoneHomeNum { get; set; }

        [Required]
        [Display(Name = "Телефон(мобильный)")]
        public string PhoneMobile { get; set; }

        [Display(Name = "Телефон(ОС)")]
        public string PhoneOS { get; set; }

        [Display(Name = "Телефон(рабочий городской)")]
        public string PhoneGTS { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        

        
        public HttpPostedFileBase ImgFile { get; set; }

        
        public Guid LoginID { get; set; }
        public int KeyRingId { get; set; }
    }

    
}