namespace TXCFluValidation.Models
{
    public class SKU
    {
        public int SKUId { get; set; }
        public string SKUCode { get; set; }
        public IEnumerable<ProductDto> ProductList { get; set; } 
        public Address AddressInfo { get; set; }
    }
}
