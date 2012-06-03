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

    public class RegForm : Ancestor    //: DefaultModelBinder
    {
        public  RegForm()
        {
            fillCollections();
        }

        
        [Required]
        [Display(Name = "Фамилия")]
        [BindingProperty("LastName")]
        public string LastName { get; set; }

        [Required]
        [BindingProperty("FirstName")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        
        [Display(Name = "Отчество")]
        public string ParentName { get; set; }
        
        [Display(Name = "Дата рождения")]
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

        [Display(Name = "Звание")]
        [FillList(typeof(MilitaryDegree),"DegreeName")]
        public SelectList UserDegree { get; set; }

        public int SelectedDegreeID { get; set; }
        
        public Guid LoginID { get; set; }
        public int KeyRingId { get; set; }
        
    }

    
}