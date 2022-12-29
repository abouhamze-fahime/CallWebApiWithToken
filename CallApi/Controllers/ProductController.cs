using CallApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CallApi.Models.ProductRepository;

namespace CallApi.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private ProductRepository _product;
        public ProductController()
        {
            _product = new ProductRepository();
        }
        public IActionResult Index()
        {
            var products = _product.GetProducts();
            return View("Index");
        }

        public IActionResult GetReport()
        {
            List<Products> Result = _product.GetProducts().ToList();
            return Json(Result);
        }

        [HttpGet("load-product-modal-body")]
        public IActionResult LoadProductModalBody(int id)
        {
            if (id==0)
            {
                Products product = new Products()
                {
                    ProductId = 0
                };
                return PartialView("_ProductModalPartial", product);
            }
            else
            {
                var product = _product.GetProduct(id);
                return PartialView("_ProductModalPartial" , product);
            }
        }


        [HttpPost("submit-product-modal")]
        public IActionResult CreateOrEdit(Products product)
        {
            try
            {
                if (product.ProductId == 0)
                {
                    _product.InsertProduct(product);
                    return new JsonResult(new { status = "Success" });
                }
                else
                {
                    _product.EditProduct(product);
                    return new JsonResult(new { status = "Success" });
                }
            }
            catch (Exception)
            {

                return new JsonResult(new { status = "Error" });
            }
        }


        [HttpGet("delete-product")]
        public IActionResult DeleteProduct ( int productId)
        {
            try
            {
                _product.DeleteProduct(productId);
                return new JsonResult(new { status = "Success" });
            }
            catch (Exception)
            {
                return new JsonResult(new { status = "Error" });
            }
        }
    }
}
