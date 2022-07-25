using System;
using System.ComponentModel.DataAnnotations;

namespace OganiProject.ViewModels.Accaunt
{
    public class ResetPasswordVM
    {
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
