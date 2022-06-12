using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.InvoiceDetails
{
    public interface ICTDonDatHangService : IBasicService<long, ChiTietDonDatHang, ChiTietDonDatHangDto, TimKiemCTDonDatHangDto>
    {
    }
}
