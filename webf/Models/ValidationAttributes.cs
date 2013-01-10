
using System;
using System.ComponentModel.DataAnnotations;

using System.IO;
using System.Web;
using System.Linq;
using System.Web.Mvc;

namespace webf.Models
{

    public class FileAttribute : ValidationAttribute
    {

        public int MaxContentLength = int.MaxValue;
        public string[] AllowedFileExtensions;
        public string[] AllowedContentTypes;

        public override bool IsValid(object value)
        {

            var file = value as HttpPostedFileBase;

            //this should be handled by [Required]
            if (file == null)
                return false;

            if (file.ContentLength > MaxContentLength)
            {
                ErrorMessage = string.Format("Размер файла большой и превысил: {0} KB",MaxContentLength/1024);
                return false;
            }

            if (AllowedFileExtensions != null)
            {
                if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                {
                    ErrorMessage = "Пожалуйста, загрузите файл с расширением: " + string.Join(", ", AllowedFileExtensions);
                    return false;
                }
            }

            if (AllowedContentTypes != null)
            {
                if (!AllowedContentTypes.Contains(file.ContentType))
                {
                    ErrorMessage = "Тип файла должен быть: " + string.Join(", ", AllowedContentTypes);
                    return false;
                }
            }

            return true;
        }
    }
}