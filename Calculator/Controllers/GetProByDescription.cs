using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using RestSharp;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

namespace Calculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class GetProByDescription : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello World");
        }

        [HttpGet("GetProduc")]
        public string Post(string name)
        {
            var client = new RestClient("https://fe.gs1-hq.mk101.signature-it.com/external/app_query/select_query.json");
            var request = new RestRequest("https://fe.gs1-hq.mk101.signature-it.com/external/app_query/select_query.json",Method.Post);
            request.AddHeader("Authorization", "Basic VG9wYXo6Zk82QDE3WDQ=");
            request.AddHeader("Access-Control-Allow-Origin", "*");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "SIGSID=t6elmgal7gi6sffbc3k35e6jb7");
            var body = @"{
" + "\n" +
            @"    ""query"": ""Trade_Item_Description like '" +name+ @"%'"",
" + "\n" +
            @"    ""get_chunks"": { ""start"": 0, ""rows"": 100 }
" + "\n" +
            @"}";

           
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            return response.Content;


        }

        [HttpGet("GetDetails")]
        public string GetProductDetailsByProductCode(string productCode)
        {
            string url = "https://fe.gs1-retailer.mk101.signature-it.com/external/product/" + productCode + ".json?hq=1";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Authorization", "Basic VG9wYXo6Zk82QDE3WDQ=");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "SIGSID=t6elmgal7gi6sffbc3k35e6jb7");
            RestResponse response = client.Execute(request);
            return response.Content;
        }

        [HttpGet("GetImage")]
        public string GetProductImageByProductCodeandMedia(string productCode)
        {
            string res = GetProductDetailsByProductCode(productCode);
            dynamic dynamicObject = JsonConvert.DeserializeObject(res);
            var media = dynamicObject[0]["media_assets"][1]["id"];
            string url = "https://fe.gs1-retailer.mk101.signature-it.com/external/product/"+ productCode + "/files?media="+ media+ "&hq=1";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Authorization", "Basic VG9wYXo6Zk82QDE3WDQ=");
            request.AddHeader("Content-Type", "application/jpg");
            request.AddHeader("Cookie", "SIGSID=t6elmgal7gi6sffbc3k35e6jb7");
            RestResponse response = client.Execute(request);
            JObject v = JObject.Parse(response.Content);
            string b = v.GetValue("file").ToString();
            return b;
        }

    }
}
