using System;
using Microsoft.AspNetCore.Identity;

namespace OganiProject.Entities
{
    public class AppRole:IdentityRole<int>
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
