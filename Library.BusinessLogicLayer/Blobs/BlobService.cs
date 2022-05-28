﻿using AutoMapper;
using Library.Common.Dtos;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Blobs
{
    public class BlobService : BasicService<long, Blob, BlobDto, PageRequestDto>, IBlobService
    {
        private readonly WebShopDbHelper _dbHelper;

        public BlobService(WebShopDbHelper dbHelper, IMapper mapper) : base(dbHelper.Blobs, mapper)
        {
            _dbHelper = dbHelper;
        }

        public BlobDto FindPath(string path)
        {
            return this.GetList(1, new DbQueryParameterGroup(new DbQueryParameter(nameof(Blob.File_Path), path))).FirstOrDefault();
        }
    }
}
