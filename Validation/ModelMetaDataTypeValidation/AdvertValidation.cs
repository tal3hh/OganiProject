using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OganiProject.Validation.ModelMetaDataTypeValidation
{
    public class AdvertValidation
    {
        [Required(ErrorMessage = "Photo bos kecmeyin.")]
        public IFormFile Photo { get; set; }
    }
}
