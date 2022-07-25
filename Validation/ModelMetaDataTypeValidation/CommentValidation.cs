using System;
using System.ComponentModel.DataAnnotations;

namespace OganiProject.Validation.ModelMetaDataTypeValidation
{
    public class CommentValidation
    {
        [Required(ErrorMessage = "Message bos kecmeyin.")]
        [MaxLength(1500, ErrorMessage = "Max 1500 simvol ola biler.")]
        public string Message { get; set; }
    }
}
