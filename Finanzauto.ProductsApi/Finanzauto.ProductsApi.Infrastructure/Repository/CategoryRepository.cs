using Finanzauto.ProductsApi.Domain.Entities;
using Finanzauto.ProductsApi.Domain.Interfaces;
using Finanzauto.ProductsApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzauto.ProductsApi.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
               _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
                return await _context.Categories.ToListAsync();
      
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
                return await _context.Categories.FindAsync(id);
         
        }
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
