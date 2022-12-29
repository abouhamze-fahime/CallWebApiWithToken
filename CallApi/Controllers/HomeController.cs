using CallApi.Models;
using CallApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CallApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CustomerRepository _customer;
        private IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _customer = new CustomerRepository();
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CustomerList()
        {
            Token token = new Token(_httpClientFactory);
            LoginViewModel us = new LoginViewModel();
            us.UserName = "Administrator";
            us.Password = "123";
           var to=  token.GetToken(us);
            var to2 = token.GetToken2(us);
           // string token = User.FindFirst("AccessKey").Value;
            var customerlst = _customer.GetCustomers(to);
            return View(customerlst);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet("load-customer-modal-body")]
        public IActionResult LoadCustomerModalBody(int customerId)
        {
            if (customerId == 0)
            {
                Customer customer = new Customer()
                {
                    CustomerId = 0
                };
                return PartialView("_CustomerModalPartial", customer);
            }
            else
            {
                var customer = _customer.GetCustomer(customerId);
                // _customer.EditCustomer(customer);
                return PartialView("_CustomerModalPartial", customer);
            }
        }

        [HttpPost("submit-customer-modal")]
        public IActionResult CreateOrEdit(Customer customer)
        {
            try
            {
                if (customer.CustomerId == 0)
                {
                    _customer.AddCustomer(customer);
                    return new JsonResult(new { status = "Success" });
                }
                else
                {
                    _customer.EditCustomer(customer);
                    return new JsonResult(new { status = "Success" });
                }
            }
            catch (Exception)
            {

                return new JsonResult(new { status = "Error" });
            }
        }

        [HttpGet("DeleteCustomer/{customerId}")]
        public IActionResult DeleteCustomer(int customerId)
        {
            try
            {
                _customer.DeleteCustomer(customerId);
                return new JsonResult(new { status = "Success" });
            }
            catch (Exception)
            {
                return new JsonResult(new { status = "Error" });
            }

        }
    }
}
