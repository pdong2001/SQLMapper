using Library.Common.Dtos;
using System;
using System.Collections.Generic;
using Library.DataModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Products
{
    public interface IProductService : IBasicService<long, Product, ProductDto, PageRequestDto>
    {
    }
}
