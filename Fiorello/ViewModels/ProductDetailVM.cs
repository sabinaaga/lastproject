namespace Fiorello.ViewModels
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<ProductImageVM> ProductImages { get; set; }
        public string CategoryName { get; set; }

    }
}
