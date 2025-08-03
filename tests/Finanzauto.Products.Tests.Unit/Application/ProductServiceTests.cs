using Finanzauto.Products.Application.Services;
using Finanzauto.ProductsApi.Domain.Entities;
using Finanzauto.ProductsApi.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzauto.Products.Tests.Application
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly Mock<IMemoryCache> _mockCache;
        private readonly ProductService _service;
        public ProductServiceTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _mockCache = new Mock<IMemoryCache>();
            _service = new ProductService(_mockRepo.Object, _mockCache.Object);
        }
        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenProductExists()
        {
            var product = new Product { Id = 1 };
            _mockRepo.Setup(r => r.GetByIdProductAsync(1)).ReturnsAsync(product);
            _mockRepo.Setup(r => r.DeleteProductAsync(1)).ReturnsAsync(1);

            var result = await _service.DeleteProductAsync(1);

            Assert.Equal(1, result);
        }
        [Fact]
        public async Task GetAllProductsAsync()
        {

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product A", Description = "Description A", Price = 10.5M, Stock = 5, CategoryId = 1},
                new Product { Id = 2, Name = "Product B", Description = "Description B", Price = 20.0M, Stock = 10, CategoryId= 1}
            };

            _mockRepo.Setup(r => r.GetAllProductAsync()).ReturnsAsync(products);
            var result = await _service.GetAllProductAsync();
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Product A", result.First().Name);
        }
    }
}
