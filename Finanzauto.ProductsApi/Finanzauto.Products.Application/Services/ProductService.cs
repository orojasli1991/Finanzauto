using Finanzauto.Products.Application.Dto;
using Finanzauto.ProductsApi.Domain.Entities;
using Finanzauto.ProductsApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Finanzauto.Products.Application.Models;

namespace Finanzauto.Products.Application.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository, IMemoryCache cache)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductAsync()
        {
            var products = await _repository.GetAllProductAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                //CategoryName = p.Category.CategoryName
                

            });
        }
        public async Task<ProductDto> GetByProductIdAsync(int id)
        {
            var product = await _repository.GetByIdProductAsync(id);
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };
        }
        public async Task<int> CreateProductAsync(ProductDto dto)
        {
            if (dto.Price <= 0 || dto.Stock <= 0)
                throw new ArgumentException("Precio y Stock deben ser mayores a cero");

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId,
                CreateDate= DateTime.UtcNow
            };
            return await _repository.CreateProductAsync(product);
        }
        public async Task<int> UpdateProductAsync(int id, ProductDto dto)
        {
            var product = new Product
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId
            };
            return await _repository.UpdateProductAsync(product);
        }
        public async Task<int> DeleteProductAsync(int id)
        {
            return await _repository.DeleteProductAsync(id);
        }
    }
}
