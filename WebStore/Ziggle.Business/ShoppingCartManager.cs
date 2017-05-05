using System.Linq;
using Ziggle.Repository;

namespace Ziggle.Business
{
    public class ShoppingCartModel
    {
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }

    public interface IShoppingCartManager
    {
        ShoppingCartModel Add(int userId, int productId, int quantity);
        ShoppingCartModel[] GetAll(int userId);
        bool Remove(int userId, int productId);
    }

    public class ShoppingCartManager : IShoppingCartManager
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IProductManager productManager;

        public ShoppingCartManager(IShoppingCartRepository shoppingCartRepository, IProductManager productManager)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productManager = productManager;
        }

        public ShoppingCartModel Add(int userId, int productId, int quantity)
        {
            var item = shoppingCartRepository.Add(userId, productId, quantity);

            return new ShoppingCartModel { ProductId = item.ProductId, UserId = item.UserId, Quantity = item.Quantity };
        }

        public ShoppingCartModel[] GetAll(int userId)
        {
            var items = shoppingCartRepository.GetAll(userId)
                .Select(t =>{
                    var product = productManager.GetProduct(t.ProductId);
                    return new ShoppingCartModel { UserId = t.UserId, ProductName= product.Name, ProductId = t.ProductId,ProductPrice = product.Price, Quantity = t.Quantity };
                })
                .ToArray();

            return items;
        }

        public bool Remove(int userId, int productId)
        {
            return shoppingCartRepository.Remove(userId, productId);
        }
    }
}
