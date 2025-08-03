using Finanzauto.ProductsApi.Domain.Entities;
using Finanzauto.ProductsApi.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finanzauto.ProductsApi.Domain.Interfaces;
using System.Data.Common;

namespace Finanzauto.ProductsApi.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly string _connectionString;

        public ProductRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            var products = _context.Products.AsQueryable();

            return (products);

        }
        public async Task<Product> GetByIdProductAsync(int id)
        {
            return await _context.Products
           .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<int> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }
        public async Task<int> UpdateProductAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);

            if (existingProduct == null)
                throw new KeyNotFoundException($"Producto con ID {product.Id} no encontrado.");

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.CategoryId = product.CategoryId;

            _context.Products.Update(existingProduct);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteProductAsync(int id)
        {
            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
                throw new KeyNotFoundException($"Producto con ID {id} no encontrado.");

            _context.Products.Remove(existingProduct);
            return await _context.SaveChangesAsync();
        }
    }
}
