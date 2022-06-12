﻿using Library.BusinessLogicLayer.ProductDetails;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Products
{
    public class DoUongDto : DoUong
    {
        public Blob Image { get; set; }
        public IList<ChiTietSanPhamDto> Details { get; set; }
        public LoaiDoUong Category { get; internal set; }
    }
}