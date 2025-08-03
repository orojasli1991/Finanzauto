using Finanzauto.Products.Application.Dto;
using Finanzauto.ProductsApi.Domain.Entities;
using Finanzauto.ProductsApi.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Finanzauto.Products.Application.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMemoryCache _cache;

        public CategoryService(ICategoryRepository repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<CategoryDto> CreateAsync(CategoryDto inputcategory)
        {
            var category = new Category
            {
                CategoryName = inputcategory.CategoryName,
                Description = inputcategory.Description,

            };        
            var created = await _repository.CreateAsync(category);

            return new CategoryDto
            {
                CategoryId = created.CategoryId,
                CategoryName = created.CategoryName,
                Description = created.Description

            };
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return null;            
            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description,               
            };
        }
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            const string cacheKey = "AllCategories";
            if (_cache.TryGetValue(cacheKey, out List<CategoryDto> cachedCategories))
            {
                return cachedCategories;
            }
            var categorys = await _repository.GetAllAsync();
            var categoryDtos= categorys.Select(p => new CategoryDto
            {
                CategoryId = p.CategoryId,
                CategoryName = p.CategoryName,
                Description = p.Description,              

            }).ToList();
            _cache.Set(cacheKey, categoryDtos, TimeSpan.FromMinutes(1));
            return categoryDtos;
        }
        public async Task<bool> UpdateAsync(Category category)
        {
            var existing = await _repository.GetByIdAsync(category.CategoryId);
            if (existing == null) return false;

            existing.CategoryName = category.CategoryName;
            existing.Description = category.Description;

            await _repository.UpdateAsync(existing);
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

    }
}
