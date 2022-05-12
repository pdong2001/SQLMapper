using AutoMapper;
using Library.Common.Dtos;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.ProductDetails
{
    public class ProductDetailService : BasicService<long, ProductDetail, ProductDetailDto, PageRequestDto>, IProductDetailService
    {
        private readonly WebShopDbHelper _context;

        public ProductDetailService(WebShopDbHelper context, IMapper mapper) : base(context.ProductsDetails, mapper)
        {
            _context = context;
        }


    }
}
