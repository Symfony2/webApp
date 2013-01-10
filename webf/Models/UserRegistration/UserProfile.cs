using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webf.Models.EntityModel
{
    [MetadataType(typeof(UserProfileMetadata)) ]
    public partial class UserProfile
    {
        
    }

    public class UserProfileMetadata
    {
        [DisplayName("Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}