using Finanzauto.ProductsApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzauto.ProductsApi.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<Category> CreateAsync(Category category);
        public Task<IEnumerable<Category>> GetAllAsync();
        public Task<Category?> GetByIdAsync(int id);
        public Task UpdateAsync(Category category);
        public Task DeleteAsync(int id);
    }
}
