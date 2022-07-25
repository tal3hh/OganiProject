using System;
using OganiProject.Entities;

namespace OganiProject.ViewModels.Admin.ProductViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
       
    }
}
