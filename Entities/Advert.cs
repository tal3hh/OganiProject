using System;
using System.ComponentModel.DataAnnotations.Schema;
using OganiProject.Validation.ModelMetaDataTypeValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OganiProject.Entities
{
    [ModelMetadataType(typeof(AdvertValidation))]
    public class Advert:BaseEntity
    {
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
