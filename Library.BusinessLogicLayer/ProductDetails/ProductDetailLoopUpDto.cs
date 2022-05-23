using Library.Common.Dtos;

namespace Library.BusinessLogicLayer.ProductDetails
{
    public class ProductDetailLoopUpDto : PageRequestDto
    {
        public bool With_Product { get; set; } = false;
    }
}