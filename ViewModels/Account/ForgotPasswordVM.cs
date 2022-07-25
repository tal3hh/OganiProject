using System;
using System.ComponentModel.DataAnnotations;

namespace OganiProject.ViewModels.Accaunt
{
    public class ForgotPasswordVM
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
