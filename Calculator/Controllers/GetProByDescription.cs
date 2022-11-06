using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using RestSharp;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Data;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.RegularExpressions;

namespace Calculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class GetProByDescription : ControllerBase
    {
        private IMongoCollection<Producs> _producs;

        public GetProByDescription (IMongoClient client)
        {
            var mongodb = client.GetDatabase("Producs");
            _producs = mongodb.GetCollection<Producs>("Producs");
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello World");
        }

        [HttpGet("GetProduc")]
        public string Post(string name,int start)
        {
            int row = 5;
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
            @"    ""get_chunks"": { ""start"":"+ start+@", ""rows"":"+ row+@" }
" + "\n" +
            @"}";

           
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            if(response.Content.Length==4)
            { 
                var request2 = new RestRequest("https://fe.gs1-hq.mk101.signature-it.com/external/app_query/select_query.json", Method.Post);
                request2.AddHeader("Authorization", "Basic VG9wYXo6Zk82QDE3WDQ=");
                request2.AddHeader("Access-Control-Allow-Origin", "*");
                request2.AddHeader("Content-Type", "application/json");
                request2.AddHeader("Cookie", "SIGSID=t6elmgal7gi6sffbc3k35e6jb7");

                body = @"{
" + "\n" +
            @"    ""query"": ""Trade_Item_Description like '%" + name + @"%'"",
" + "\n" +
            @"    ""get_chunks"": { ""start"":" + start + @", ""rows"":" + row + @" }
" + "\n" +
            @"}";
                request2.AddParameter("application/json", body, ParameterType.RequestBody);
                response = client.Execute(request2);

            }

            return response.Content;


        }

        [HttpGet("GetProducStart")]
        public string GetProducStart(string name, int start)
        {
            int row = 5;
            var client = new RestClient("https://fe.gs1-hq.mk101.signature-it.com/external/app_query/select_query.json");
            var request = new RestRequest("https://fe.gs1-hq.mk101.signature-it.com/external/app_query/select_query.json", Method.Post);
            request.AddHeader("Authorization", "Basic VG9wYXo6Zk82QDE3WDQ=");
            request.AddHeader("Access-Control-Allow-Origin", "*");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "SIGSID=t6elmgal7gi6sffbc3k35e6jb7");
            var body = @"{
" + "\n" +
            @"    ""query"": ""Trade_Item_Description like '" + name + @"%'"",
" + "\n" +
            @"    ""get_chunks"": { ""start"":" + start + @", ""rows"":" + row + @" }
" + "\n" +
            @"}";


            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            return response.Content;


        }

        [HttpGet("GetProducContain")]
        public string GetProducContain(string name, int start)
        {
            int row = 5;
            var client = new RestClient("https://fe.gs1-hq.mk101.signature-it.com/external/app_query/select_query.json");
            var request = new RestRequest("https://fe.gs1-hq.mk101.signature-it.com/external/app_query/select_query.json", Method.Post);
            request.AddHeader("Authorization", "Basic VG9wYXo6Zk82QDE3WDQ=");
            request.AddHeader("Access-Control-Allow-Origin", "*");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "SIGSID=t6elmgal7gi6sffbc3k35e6jb7");
            var body = @"{
" + "\n" +
            @"    ""query"": ""Trade_Item_Description like '%" + name + @"%'"",
" + "\n" +
            @"    ""get_chunks"": { ""start"":" + start + @", ""rows"":" + row + @" }
" + "\n" +
            @"}";


            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            return response.Content;


        }

        



        [HttpGet("GetProducMongoDb")]
        public string GetProducMongoDb(string serch, int start)
        {
            //MongoClient dbClient = new MongoClient("mongodb+srv://mor0981:m12661266@cluster0.5ze2god.mongodb.net/?retryWrites=true&w=majority");
            //var dblist = dbClient.ListDatabases().ToList();
            //var mongodb = dbClient.GetDatabase("Producs");
            //var producs = mongodb.GetCollection<Producs>("Producs");
            //var builder = Builders<BsonDocument>.Filter;
            var filter = new BsonDocument("Trade_Item_Description", new Regex(serch));
            var p = _producs.Find(filter).Limit(5).Skip(start).ToList();
            var json = JsonConvert.SerializeObject(p);
            return json.ToString();
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
            //var media = dynamicObject[0]["media_assets"][1]["id"];
            var media = dynamicObject[0]["media_assets"];
            for(int i = 0; i < media.Count; i++)
            {
                if(media[i]["file_extension"].ToString() == "jpg")
                {
                    media = dynamicObject[0]["media_assets"][i]["id"];
                    string url = "https://fe.gs1-retailer.mk101.signature-it.com/external/product/" + productCode + "/files?media=" + media + "&hq=1";
                    var client = new RestClient(url);
                    var request = new RestRequest(url, Method.Get);
                    request.AddHeader("Authorization", "Basic VG9wYXo6Zk82QDE3WDQ=");
                    request.AddHeader("Content-Type", "application/jpg");
                    request.AddHeader("Cookie", "SIGSID=t6elmgal7gi6sffbc3k35e6jb7");
                    RestResponse response = client.Execute(request);
                    //return response.Content; 
                    JObject v = JObject.Parse(response.Content);
                    string b = v.GetValue("file").ToString();
                    return b;
                }
            }
            return "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";

            //string url = "https://fe.gs1-retailer.mk101.signature-it.com/external/product/"+ productCode + "/files?media="+ media+ "&hq=1";
            //var client = new RestClient(url);
            //var request = new RestRequest(url, Method.Get);
            //request.AddHeader("Authorization", "Basic VG9wYXo6Zk82QDE3WDQ=");
            //request.AddHeader("Content-Type", "application/jpg");
            //request.AddHeader("Cookie", "SIGSID=t6elmgal7gi6sffbc3k35e6jb7");
            //RestResponse response = client.Execute(request);
            //JObject v = JObject.Parse(response.Content);
            //string b = v.GetValue("file").ToString();
            //return b;
        }
    }
}
