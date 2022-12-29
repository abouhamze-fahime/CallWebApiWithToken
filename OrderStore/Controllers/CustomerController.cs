using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderStore.Domain.Interfaces;
using OrderStore.Domain.Models.EshopModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
   [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<Customers> GetCustomer([FromRoute] int id) //input id must be the same as rout
        {
            return await _unitOfWork.Customer.GetAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Customers>> GetAllCustomers()
        {
            var result = await _unitOfWork.Customer.GetAllAsync();
            Request.HttpContext.Response.Headers.Add("x_Count", _unitOfWork.Customer.CustomerCount().ToString());
            return result;
        }

        [HttpPost]  //(nameof(CreateCustomer))
        public async Task<IActionResult> CreateCustomer( [FromBody]  Customers customer) // I do not know why we use frombody here because in this case there is not make any sense 
        {
            var result = _unitOfWork.Customer.AddAsync(customer);
            await _unitOfWork.Save();
            if (result is not null)
                return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);  // new ObjectResult(result.Result); /// both return ways is correct but I'll think the first one is so efficent
            else return BadRequest("Error in Creating the Customer");
        }

        [HttpPut]
        public async Task<IActionResult>   UpdateCustomer( [FromBody]  Customers customer) //why and when we use [frombody]
        {
            _unitOfWork.Customer.Update(customer);
             await   _unitOfWork.Save();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _unitOfWork.Customer.Delete(id);
                await _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }



    }
}
