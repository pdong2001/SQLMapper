﻿using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Categories
{
    public class LoaiSanPhamDto : LoaiSanPham
    {
        public Blob Image { get; set; }
    }
}
