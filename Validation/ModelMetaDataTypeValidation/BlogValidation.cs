using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OganiProject.Validation.ModelMetaDataTypeValidation
{
    public class BlogValidation
    {
        [Required(ErrorMessage = "Title bos kecmeyin.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Photo bos kecmeyin.")]
        public IFormFile Photo { get; set; }
    }
}
