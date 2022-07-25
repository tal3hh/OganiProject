using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OganiProject.Validation.ModelMetaDataTypeValidation
{
    public class CategoryValidation
    {
        [Required(ErrorMessage = "Name bos kecmeyin.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Photo bos kecmeyin.")]
        public IFormFile Photo { get; set; }
    }
}
