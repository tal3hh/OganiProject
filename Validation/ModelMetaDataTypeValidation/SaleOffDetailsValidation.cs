using System;
using System.ComponentModel.DataAnnotations;

namespace OganiProject.Validation.ModelMetaDataTypeValidation
{
    public class SaleOffDetailsValidation
    {
        [Required(ErrorMessage = "Description bos kecmeyin.")]
        [MaxLength(2500, ErrorMessage = "Max 2500 simvol ola biler.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Weight bos kecmeyin.")]
        public decimal Weight { get; set; }
        [Required(ErrorMessage = "StarCount bos kecmeyin.")]
        public int StarCount { get; set; }
        [Required(ErrorMessage = "Availability bos kecmeyin.")]
        public bool Availability { get; set; }
    }
}
