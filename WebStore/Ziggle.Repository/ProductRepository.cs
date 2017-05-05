using System.Linq;

namespace Ziggle.Repository
{    
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public interface IProductRepository
    {
        ProductModel[] ForCategory(int categoryId);
        ProductModel GetProduct(int productId);
    }

    public class ProductRepository : IProductRepository
    {
        public ProductModel[] ForCategory(int categoryId)
        {
            return DatabaseAccessor.Instance.Categories.First(t => t.CategoryId == categoryId)
                                  .Products.Select(t =>
                                        new ProductModel
                                        {
                                            Id = t.ProductId,
                                            Name = t.ProductName,
                                            Price = t.ProductPrice,
                                            Quantity = t.ProductQuantity
                                        })
            .ToArray();
        }

        public ProductModel GetProduct(int productId)
        {
            return DatabaseAccessor.Instance.Products
                        .Where(t => t.ProductId == productId)
                        .Select(t => new ProductModel
                        {
                            Id = t.ProductId,
                            Name = t.ProductName,
                            Price = t.ProductPrice,
                            Quantity = t.ProductQuantity
                        })
                        .First();
        }
    }
}
