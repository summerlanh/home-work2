﻿using System.Linq;
using Ziggle.Repository;

namespace Ziggle.Business
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public interface IProductManager
    {
        ProductModel[] ForCategory(int categoryId);
        ProductModel GetProduct(int productId);
    }

    public class ProductManager : IProductManager
    {
        private readonly IProductRepository productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ProductModel[] ForCategory(int categoryId)
        {
            return productRepository.ForCategory(categoryId).Select(t =>
                            new ProductModel
                            {
                                Id = t.Id,
                                Name = t.Name,
                                Price = t.Price,
                                Quantity = t.Quantity
                            })
                            .ToArray();
        }

        public ProductModel GetProduct(int productId)
        {
            var product = productRepository.GetProduct(productId);

            return new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }
    }
}