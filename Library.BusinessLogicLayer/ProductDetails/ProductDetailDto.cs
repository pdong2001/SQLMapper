using Library.DataModels;

namespace Library.BusinessLogicLayer.ProductDetails
{
    public class ProductDetailDto : ProductDetail
    {
        public Product Product { get; set; }
        public new Blob Default_Image { get; set; }
        public ProductDetailOptionValue option_value { get; set; }
    }
}