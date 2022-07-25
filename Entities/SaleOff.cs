using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OganiProject.Validation.ModelMetaDataTypeValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OganiProject.Entities
{
    [ModelMetadataType(typeof(SaleOffValidation))]
    public class SaleOff:BaseEntity
    {
        public string Name { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }


        //Relation Property
        public SaleOffDetail SaleOffDetails { get; set; }
    }
}
