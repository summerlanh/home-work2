using System.Linq;

namespace Ziggle.Repository
{    
    public class ShoppingCartModel
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public interface IShoppingCartRepository
    {
        ShoppingCartModel Add(int userId, int productId, int quantity);
        ShoppingCartModel[] GetAll(int userId);
        bool Remove(int userId, int productId);
    }

    public class ShoppingCartRepository : IShoppingCartRepository
    {
        public ShoppingCartModel Add(int userId, int productId, int quantity)
        {
            var items = DatabaseAccessor.Instance.ShoppingCartItems
                .Where(t => t.UserId == userId && t.ProductId == productId);
            int newQuantity = quantity;

            if (items.Count() != 0)
            {
                DatabaseAccessor.Instance.ShoppingCartItems.Remove(items.First());
                newQuantity = items.First().Quantity + quantity;
            }

            var item = DatabaseAccessor.Instance.ShoppingCartItems.Add(
            new Ziggle.ProductDatabase.ShoppingCartItem { ProductId = productId, UserId = userId, Quantity = newQuantity });

            DatabaseAccessor.Instance.SaveChanges();

            return new ShoppingCartModel { UserId = item.UserId, ProductId = item.ProductId, Quantity = item.Quantity };
        }

        public ShoppingCartModel[] GetAll(int userId)
        {
            var items = DatabaseAccessor.Instance.ShoppingCartItems
                .Where(t => t.UserId == userId)
                .Select(t => new ShoppingCartModel { UserId = t.UserId, ProductId = t.ProductId, Quantity = t.Quantity })
                .ToArray();
            return items;
        }

        public bool Remove(int userId, int productId)
        {
            var items = DatabaseAccessor.Instance.ShoppingCartItems
                                .Where(t => t.UserId == userId && t.ProductId == productId);

            if (items.Count() == 0)
            {
                return false;
            }

            DatabaseAccessor.Instance.ShoppingCartItems.Remove(items.First());

            DatabaseAccessor.Instance.SaveChanges();

            return true;
        }
    }
}