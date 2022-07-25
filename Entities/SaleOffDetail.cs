using System;
using OganiProject.Validation.ModelMetaDataTypeValidation;
using Microsoft.AspNetCore.Mvc;

namespace OganiProject.Entities
{
    [ModelMetadataType(typeof(SaleOffDetailsValidation))]
    public class SaleOffDetail:BaseEntity
    {
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public bool Availability { get; set; }
        public int StarCount { get; set; }


        public int SaleOffId { get; set; }

        //Relation Property

        public SaleOff SaleOff { get; set; }
    }
}
