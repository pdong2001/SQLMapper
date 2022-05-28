﻿using Library.Common.Dtos;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.ProductDetails
{
    public interface IProductDetailService : IBasicService<long, ProductDetail, ProductDetailDto, PageRequestDto>
    {
    }
}
