using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CallApi.Models
{
    public class ProductRepository
    {
        private string apiUrl = "https://localhost:44388/api/Product";
        private HttpClient _client;
        public ProductRepository()
        {
            _client = new HttpClient();
        }

        public IEnumerable<Products> GetProducts()
        {
           // _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = _client.GetStringAsync(apiUrl ).Result;
            List<Products> products = JsonConvert.DeserializeObject<List<Products>>(result);
            return products;

        }


        public Products GetProduct(int id)
        {
            var result = _client.GetStringAsync(apiUrl +"/" + id).Result;
            Products product = JsonConvert.DeserializeObject<Products>(result);
            return product;
        }


        public void InsertProduct(Products products)
        {
            var productJson = JsonConvert.SerializeObject(products);
            StringContent content = new StringContent(productJson, Encoding.UTF8, "application/json");
            var result = _client.PostAsync(apiUrl + "/CreateProduct" , content).Result;
        }


        public void EditProduct (Products products)
        {
            var productJson = JsonConvert.SerializeObject(products);
            StringContent content = new StringContent(productJson, Encoding.UTF8, "application/json");
            var result = _client.PutAsync(apiUrl + "/UpdateProduct", content).Result;
        }

        public void DeleteProduct(int id)
        {
            var result = _client.DeleteAsync(apiUrl + "/DeleteProduct?id=" + id);
        }



        public class Products
        {
            public int ProductId { get; set; }
            [Display(Name = "Product Name")]
            [Required(ErrorMessage = "{0} is required!")]
            public string ProductName { get; set; }
            public string Description { get; set; }
            public int CategoryId { get; set; }
            public int StockId { get; set; }


        }




    }
}
