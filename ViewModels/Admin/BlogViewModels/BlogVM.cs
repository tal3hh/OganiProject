using System;
using OganiProject.Entities;

namespace OganiProject.ViewModels.Admin.BlogViewModels
{
    public class BlogVM
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }

        public BlogDetail BlogDetails { get; set; }
    }
}
