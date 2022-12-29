using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using CallApi.ViewModels;

namespace CallApi.Models
{
    public class CustomerRepository
    {

        private string apiUrl = "https://localhost:44388/api/Customer";

        private HttpClient _client;
        public CustomerRepository()
        {
            _client = new HttpClient();
        }


        public IEnumerable<Customer> GetCustomers(TokenViewModel token)
        {
            
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.token);
            var result = _client.GetStringAsync(apiUrl+ "/GetAllCustomers").Result;
            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(result);
            return customers;
        }

        public Customer GetCustomer(int cusomterId)
        {
            var result = _client.GetStringAsync(apiUrl + "/GetCustomer/" + cusomterId).Result;
            Customer customer = JsonConvert.DeserializeObject<Customer>(result);
            return customer;
        }

        public void AddCustomer(Customer customer)
        {
            var customerJson = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(customerJson, Encoding.UTF8, "application/json");
            var result = _client.PostAsync(apiUrl+ "/CreateCustomer", content).Result;
        }




        public void EditCustomer(Customer customer)
        {
            var customerJson = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(customerJson, Encoding.UTF8, "application/json");
            var result = _client.PutAsync(apiUrl+ "/UpdateCustomer", content).Result;
        }

        public void DeleteCustomer(int customerId)  
        {
            var result = _client.DeleteAsync(apiUrl + "/DeleteCustomer?id=" + customerId);
        }

    }



    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(150)]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(10)]
        public string Nationalcode { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(50)]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(150)]
        public string Address { get; set; }
       
    }
}
