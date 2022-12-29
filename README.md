# CallWebApiWithToken
Calling an API via token 
1-	For user name and password we define an LoginViewModel class with two properties : 

       public class LoginViewModel
       {
           [Required]
           public string UserName { get; set; }
           [Required]
           public string Password { get; set; }
       }
2-	Define GetToken method with LoginViewModel instance as it’s input parameter. Inside it we should instantiate from HttpClient to use it for calling Token and sending username and password through it. 
         HttpClient Client = new HttpClient()
3-	 Here we should convert to json LoginViewModel:
             var jsonBody = JsonConvert.SerializeObject(login);

4-	After that we form and build our sending package. In order to do that first thing we use StringContent to make content of our package.

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

5-	Finally we send our content via PostAsync method:

       var response = client.PostAsync("/api/Token/GetToken", content).Result;


 Notice that our address is consists of two part : 
Full address : Base Address + token address:
If we want set base address in program.cs we instantiate from IHttpClientFactory and inject it through constructor beside we should set programs.cs 
services.AddHttpClient("MyWebApp", client =>
  {
               client.BaseAddress = new Uri("https://localhost:44388");
   });
We add above service in this way we can use 
var client = _httpClientFactory.CreateClient("MyWebApp");
in our get token method and only use token address in response 
var response = client.PostAsync("/api/Token/GetToken", content).Result;

but if we do not set above service in programs.cs we should use full address like response2:

var response2 = client.PostAsync("https://localhost:44388/api/Token/GetToken", content).Result; 
if IsSuccessStatusCode is true then we have a json content and in this level we fetch token text in order to do that we have several way lets go through them :

number 1:
   var token = response.Content.ReadAsStringAsync().Result;

we separate the result of response from other extra information come with response
our token is a json which has maybe several key- value so that we just want just value of token key for example 

{
 “result”:{
  “Token”:”gfgkfgkuy”,
   “res”: “true”

  },
 “createtime” : “2022-12-28”
}


In our example we have nested json which consist of two level to reach to token 
In level one we should take result part and inside it we get the token part value 
In order to do that we use code like below:

JToken jobject = JObject.Parse(token);

We create a class which contains a field that name is exactly the same as our response result:
In our case 


   public class TokenViewModel
    {
        public string Token { get; set; }
    }



 TokenViewModel c = new TokenViewModel()
  {
     Token = (jobject["result"]["Token"].ToString())
  };

So we return c

Or if we do not have nested json object we use TokenViewModel to fetch response for that we write the code bellow 


  var token1 = response.Content.ReadAsAsync<TokenViewModel>().Result;

but notice that in order to use ReadAsAsync you should install the package 
Microsoft.AspNet.WebApi.Client and use it in our class 


Finally we can use HttpRequestMessage to make content and HttpResponseMessage for response 

Two sample get token method according to above are like bellow 




