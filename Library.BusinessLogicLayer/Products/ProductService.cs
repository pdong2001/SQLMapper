using AutoMapper;
using Library.Common.Dtos;
using Library.Common.Interfaces;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Products
{
    public class ProductService : BasicService<long, Product, ProductDto, PageRequestDto>, IProductService
    {
        private readonly WebShopDbHelper _context;
        public ProductService(WebShopDbHelper context, IMapper mapper) : base(context.Products, mapper)
        {
            this._context = context;
        }
    }
}
