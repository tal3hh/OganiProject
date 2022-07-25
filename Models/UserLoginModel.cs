using System;
using System.ComponentModel.DataAnnotations;

namespace OganiProject.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Username bos kecile bilmez.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password bos kecile bilmez.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
