namespace TXCFluValidation.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public ProductType Type { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int ProgramId { get; set; }
        public bool Status { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<Supplier> SupplierList { get; set; }
    }

    public enum ProductType
    { 
        Edenred,
        ThirdParty
    }
}
