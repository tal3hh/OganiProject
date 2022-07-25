using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OganiProject.Validation.ModelMetaDataTypeValidation
{
    public class OwnerValidation
    {
        [Required(ErrorMessage = "FullName bos kecmeyin.")]
        [MaxLength(35, ErrorMessage = "Max 35 simvol ola biler.")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Photo bos kecmeyin.")]
        public IFormFile Photo { get; set; }
    }
}
