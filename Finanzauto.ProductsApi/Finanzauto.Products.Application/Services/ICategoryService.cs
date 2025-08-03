using Finanzauto.Products.Application.Dto;
using Finanzauto.ProductsApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzauto.Products.Application.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateAsync(CategoryDto inputcategory);
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<bool> UpdateAsync(Category category);
        Task<bool> DeleteAsync(int id);

    }
}
