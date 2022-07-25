using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OganiProject.Validation.ModelMetaDataTypeValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OganiProject.Entities
{
    [ModelMetadataType(typeof(BlogValidation))]
    public class Blog:BaseEntity
    {
        public string Title { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }

        public int OwnerId { get; set; }

        //Relation Property

        
        public Owner Owner { get; set; }
        public BlogDetail BlogDetails { get; set; }
    }
}
