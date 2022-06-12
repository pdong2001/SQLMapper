using Library.Common.Dtos;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Invoices
{
    public interface IDonDatHangService : IBasicService<long, DonDatHang, DonDatHangDto, TimKiemDonDatHangDto>
    {
        public long CreateWithDetail(DonDatHang entity, IList<ChiTietDonDatHang> details);
    }
}
