using System;
using System.ComponentModel.DataAnnotations;

namespace OganiProject.Models
{
    public class UserCreatedModel
    {
        [Required(ErrorMessage = "Username bos kecile bilmez.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password bos kecile bilmez.")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Tekrar password dogru deyil.")]
        public string ConfrimPassword { get; set; }
        [Required(ErrorMessage = "Email bos kecile bilmez.")]
        [EmailAddress(ErrorMessage = "Email formatinda yazi daxil edin.")]
        public string Email { get; set; }
    }
}
