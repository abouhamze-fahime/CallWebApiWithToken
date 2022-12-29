﻿using CallApi.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;
using System.Net;

namespace CallApi.Models
{
    public class Token
    {
        private IHttpClientFactory _httpClientFactory;

        public Token(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public TokenViewModel GetToken(LoginViewModel login)
        {
            var client = _httpClientFactory.CreateClient("MyWebApp");
            using (HttpClient Client = new HttpClient())
            {
                var jsonBody = JsonConvert.SerializeObject(login);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var response = client.PostAsync("/api/Token/GetToken", content).Result;
                var response2 = Client.PostAsync("https://localhost:44388/api/Token/GetToken", content).Result;
                if (response2.IsSuccessStatusCode)
                {
                    var token = response.Content.ReadAsStringAsync().Result;
                    var token2 = response2.Content.ReadAsStringAsync().Result;
                    JToken jobject = JObject.Parse(token2);
                    TokenViewModel c = new TokenViewModel()
                    {
                        token = (jobject["token"].ToString())
                    };


                    var token1 = response.Content.ReadAsAsync<TokenViewModel>().Result;
                    var t = JsonConvert.DeserializeObject(token);
                    //  JsonSerializer.Deserialize<TokenViewModel>(token);

                    return (token1);
                }
                else
                {
                    return null;
                }
            }
        }

        public string GetToken2(LoginViewModel user)
        {
            string baseAddress = "https://localhost:44388/api/Token/GetToken";
            using (HttpClient Client = new HttpClient())
            {
                using (HttpRequestMessage bodyRequest = new HttpRequestMessage(HttpMethod.Post, baseAddress))
                {
                    bodyRequest.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseApi = Client.PostAsync(baseAddress, bodyRequest.Content, new System.Threading.CancellationToken(false)).Result;
                    //if (responseApi.StatusCode == HttpStatusCode.OK)
                    //{
                    if (responseApi.IsSuccessStatusCode)
                    {
                        var finalRes = responseApi.Content.ReadAsStringAsync().Result;
                        var _Result = finalRes;

                        if (_Result != null)
                        {
                            JToken jobject = JObject.Parse(_Result);
                            TokenViewModel c = new TokenViewModel()
                            {
                                token = (jobject["token"].ToString())
                            };

                            //JObject dataa = JObject.Parse(_Result);
                            //JObject dataa2 = JObject.Parse(_Result);
                            //JToken res = dataa.SelectToken("result");
                            //ResponseText = res.ToString();
                            return c.token;
                        }
                        else
                        {
                            return "Response is null";
                        };
                    }
               // }
                    else
                    {
                        return "There is Error";
                    }
                }
            }

        }


    }
}
