using System;
using System.ComponentModel.DataAnnotations;

namespace OganiProject.Validation.ModelMetaDataTypeValidation
{
    public class ContactValidation
    {
        [Required(ErrorMessage = "Email bos kecmeyin.")]
        [EmailAddress(ErrorMessage = "Email formatinda simvol daxil edin.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Adress bos kecmeyin.")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "OpenTime bos kecmeyin.")]
        public string OpenTime { get; set; }
        [Required(ErrorMessage = "Phone bos kecmeyin.")]
        [MaxLength(15, ErrorMessage = "Max 15 simvol ola biler.")]
        public string Phone { get; set; }
    }
}
