using Library.Common.Attributes;
using Library.Common.Dtos;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Library.DataModels
{
    [TableName("Products")]
    public class Product : AuditedEntity<long>
    {
        public int Product_Category_Id { get; set; }
        public int Product_Brand_Id { get; set; }
        public int Product_Unit_Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Image_Id { get; set; }
        public string Short_Description { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public int View_Count { get; set; }
        public int Order { get; set; }
        public int Status { get; set; }
    }
}
