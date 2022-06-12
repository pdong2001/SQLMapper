using Library.Common.Dtos;

namespace Library.BusinessLogicLayer.InvoiceDetails
{
    public class TimKiemCTDonDatHangDto : PageRequestDto
    {
        public long? Invoice_Id { get; set; }
    }
}