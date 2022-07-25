using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OganiProject.Validation.ModelMetaDataTypeValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OganiProject.Entities
{
    [ModelMetadataType(typeof(ProductValidation))]
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }

        public int CategoryId { get; set; }
        public List<Comment> Comments { get; set; }

        //Relation Property

        public Category Category { get; set; }
        public ProductDetail ProductDetails { get; set; }
    }
}
