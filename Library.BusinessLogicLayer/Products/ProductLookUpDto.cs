using Library.Common.Dtos;

namespace Library.BusinessLogicLayer.Products
{
    public class ProductLookUpDto : PageRequestDto
    {
        public bool With_Detail { get; set; } = false;
        public bool Comsumable_Only { get; set; } = false;
        public bool With_Category { get; set; } = false;
        public int? Max_Price { get; set; }
        public int? Min_Price { get; set; }
        public long? Category_Id { get; set; }
    }
}