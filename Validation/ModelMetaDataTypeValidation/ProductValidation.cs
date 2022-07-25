using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OganiProject.Validation.ModelMetaDataTypeValidation
{
    public class ProductValidation
    {

        [MaxLength(15, ErrorMessage = "Max 15 simvol ola biler.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Photo bos kecmeyin.")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Price bos kecmeyin.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Count bos kecmeyin.")]
        public int Count { get; set; }
    }
}
