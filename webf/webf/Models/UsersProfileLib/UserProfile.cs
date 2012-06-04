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
        [BindingProperty("ParentName")]
        public string ParentName { get; set; }
        
        [Display(Name = "Дата рождения")]
        [BindingProperty("BirthDate")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required]
        [BindingProperty("FirstName")]
        [Display(Name = "Post")]
        public string Post { get; set; }

        [BindingProperty("FirstName")]
        [Display(Name = "Телефон(домащний)")]
        public string PhoneHomeNum { get; set; }
        
        [Required]
        [BindingProperty("FirstName")]
        [Display(Name = "Телефон(мобильный)")]
        public string PhoneMobile { get; set; }

        [BindingProperty("FirstName")]
        [Display(Name = "Телефон(ОС)")]
        public string PhoneOS { get; set; }

        [BindingProperty("FirstName")]
        [Display(Name = "Телефон(рабочий городской)")]
        public string PhoneGTS { get; set; }

        [BindingProperty("FirstName")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        
        public HttpPostedFileBase ImgFile { get; set; }

        [BindingProperty("FirstName")]
        [Display(Name = "Звание")]
        [FillList(typeof(MilitaryDegree),"DegreeName")]
        public SelectList UserDegree { get; set; }

        public int SelectedDegreeID { get; set; }
        
        public Guid LoginID { get; set; }
        public int KeyRingId { get; set; }
        
    }

    
}