using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderStore.Domain.Interfaces;
using OrderStore.Domain.Models.EshopModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            await _unitOfWork.Save();
            return (products);
        }


        [HttpGet("{id}")]
        public async Task<Product> GetProduct([FromRoute] int id) //input id must be the same as rout
        {
            return await _unitOfWork.Products.GetAsync(id);
        }


        [HttpPost(nameof(CreateProduct))]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var result = await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.Save();
            if (result is not null) return Ok("Product Created");
            else return BadRequest("Error in Creating the Product");
        }

        [HttpPut(nameof(UpdateProduct))]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            _unitOfWork.Products.Update(product);
            await _unitOfWork.Save();
            return Ok("Product Updated");
        }

        [HttpDelete(nameof(DeleteProduct))]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _unitOfWork.Products.Delete(id);
                await _unitOfWork.Save();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }



    }
}
