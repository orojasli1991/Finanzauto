using Finanzauto.Products.Application.Dto;
using Finanzauto.Products.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finanzauto.Products.Controllers
{
    [Route("api")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _service;

        public ProductController(IProductService productServcies)
        {
            _service = productServcies;
        }
        #region GET
        [HttpGet("products")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllProductAsync());
        }
        [HttpGet("products/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetByProductIdAsync(id));
        }
        #endregion
        #region POST
        [HttpPost("products")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            var result = await _service.CreateProductAsync(dto);
            return result == 0 ? NotFound() : Ok();
        }
        #endregion
        #region PUT
        [HttpPut("products/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto dto)
        {
            var result = await _service.UpdateProductAsync(id, dto);
            return result == 0 ? NotFound() : Ok();
        }
        #endregion
        #region DELETE
        [HttpDelete("products/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteProductAsync(id);
            return result == 0 ? NotFound() : Ok();
        }
        #endregion

    }
}
