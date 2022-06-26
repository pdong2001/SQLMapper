﻿using Library.Common.Dtos;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Products
{
    public interface ISanPhamService : IBasicService<long, SanPham, SanPhamDto, TimKiemSanPhamDto>
    {
    }
}