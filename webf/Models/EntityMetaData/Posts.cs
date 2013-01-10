using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace webf.Models.EntityModel
{
    [MetadataType(typeof(PostsMetadata))]
    public partial class Posts
    {

    }

    class PostsMetadata
    {
        [Required]
        [Display(Name = "Должность")]
        public string PostName { get; set; }
    }
}