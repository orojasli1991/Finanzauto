using Finanzauto.Products.Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Finanzauto.Products.Application.Dto;
using Finanzauto.ProductsApi.Domain.Entities;

namespace Finanzauto.Products.Controllers
{
    [Route("api")]
    public class CategoryController: ControllerBase
    {
        private readonly ICategoryService _categoryServices;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryServices, IMapper mapper)
        {
            _categoryServices = categoryServices;
            _mapper = mapper;
        }
        [HttpPost("categories")]
        [Authorize]    
        public async Task<IActionResult> Create([FromForm] CategoryDto inputDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                var categoryDto = await _categoryServices.CreateAsync(inputDto);
                return CreatedAtAction(nameof(GetById), new { id = categoryDto.CategoryId }, categoryDto);
           
        }
        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            
                var category = await _categoryServices.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
        }
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Categorys()
        {
                var categories = await _categoryServices.GetAllAsync();
                return Ok(categories);
        }
        [HttpPut("categories")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto dto)
        {
            //if (id != dto.CategoryId) return BadRequest("ID mismatch");

            var category = _mapper.Map<Category>(dto);
            var result = await _categoryServices.UpdateAsync(category);

            if (!result) return NotFound();
            return NoContent();
        }
        [HttpDelete("categories")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryServices.DeleteAsync(id);

            if (!result) return NotFound();
            return NoContent();
        }
    }
}
