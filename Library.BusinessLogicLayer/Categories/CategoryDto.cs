using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Categories
{
    public class CategoryDto : Category
    {
        public IList<Product> Products { get; set; }
    }
}
