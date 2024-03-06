using ASPFramework.Pages;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Web.DynamicData;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Web.Http;
using static ASPFramework.ValuesController;
using static System.Net.Mime.MediaTypeNames;

namespace ASPFramework
{
    public class ValuesController : ApiController
    {
        public class Producto
        {
            public string CodigoProducto { get; set;}
            public string Titulo { get; set; }
            public string Precio { get; set; }
        }

        public class Link
        {
            public string Next { get; set; }
            public string Previous { get; set; }
            public string Actual { get; set; }

        }
        public class Pagination
        {
            public string Item_total_count { get; set; }
            public string Item_count { get; set; }
            public string Page { get; set; }
            public string Limit { get; set; }
            public Link Links { get; set; }
        }
        public class Respuesta
        {
            public string Status_code { get; set; }
            public string Success { get; set; }
            public Pagination Pagination { get; set; }
            public List<Producto> Data { get; set; }
        }

        private class ReadAndParseJsonFileWithNewtonsoftJson
        {
            private readonly StreamReader _reader;

            public ReadAndParseJsonFileWithNewtonsoftJson(StreamReader reader)
            {
                _reader = reader;
            }
            public Respuesta UseUserDefinedObjectWithNewtonsoftJson()
            {
                var json = _reader.ReadToEnd();
                Respuesta respuesta = JsonConvert.DeserializeObject<Respuesta>(json);
                return respuesta;
            }
        }

        private string url = "http://82.98.132.218:6587/api/productos";
        private string urlFoto = "http://82.98.132.218:6587/images/";

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string GetProductos()
        {
            //return "value";
            //var url2 = $"http://localhost:4350/items";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //HttpWebResponse response = WebResponse
            request.Headers.Add("token:eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZFVzdWFyaW8iOiI2Yzc4YWYxYS0yZTdlLTQ5ZDMtYjAxNS1lZDI3YTlmNDgzNWQiLCJub21icmVVc3VhcmlvIjoidGVzdHdlYiIsImlhdCI6MTcwNTA1NzAyMSwiaXNzIjoiaHR0cHM6Ly93d3cuYWR6Z2kuY29tIiwianRpIjoiIn0.J9SasbEaxwU2hlG5YRpDEeEJc8vZgb6cVYzj3cRNo84"); 
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return "";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();                            
                            // Do something with responseBody
                            return responseBody.Remove(1,33);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                return ex.Message;
            }
        }

        public string GetFoto (string id)
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebRequest lxRequest = (HttpWebRequest)WebRequest.Create(urlFoto + id + ".jpg");
            //HttpWebResponse response = WebResponse
            lxRequest.Headers.Add("token:eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZFVzdWFyaW8iOiI2Yzc4YWYxYS0yZTdlLTQ5ZDMtYjAxNS1lZDI3YTlmNDgzNWQiLCJub21icmVVc3VhcmlvIjoidGVzdHdlYiIsImlhdCI6MTcwNTA1NzAyMSwiaXNzIjoiaHR0cHM6Ly93d3cuYWR6Z2kuY29tIiwianRpIjoiIn0.J9SasbEaxwU2hlG5YRpDEeEJc8vZgb6cVYzj3cRNo84");
            lxRequest.Method = "GET";
            lxRequest.ContentType = "image/jpeg";
            lxRequest.Accept = "image/jpeg";

            try
            {


                // returned values are returned as a stream, then read into a string
                String lsResponse = string.Empty;
                using (HttpWebResponse lxResponse = (HttpWebResponse)lxRequest.GetResponse())
                {
                    using (BinaryReader reader = new BinaryReader(lxResponse.GetResponseStream()))
                    {
                        Byte[] lnByte = reader.ReadBytes(1 * 1024 * 1024 * 10);
                        string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                        string rutaBase = Path.Combine(path, @"..\Resources\Images\");
                        string r = rutaBase.Remove(0, 6) + $"{id}.jpg";
                        using (FileStream lxFS = new FileStream(r, FileMode.OpenOrCreate))
                        {
                            lxFS.Write(lnByte, 0, lnByte.Length);
                            //return lxFS;
                            return @"..\Resources\Images\" + $"{ id}.jpg";
                        }
                    }
                }
                //MessageBox.Show("done");
                /*
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return "";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            return responseBody.Remove(1, 33);
                        }
                    }
                }
                */
            }
            catch (WebException ex)
            {
                // Handle error
                return null;
            }
        }

        public Respuesta GetProductosPaginados(string url2,string parametros)
        {
            string url3 = url2;
            if (parametros != "") { url3 = url2 + "&" + parametros; }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url3);
            request.Headers.Add("token:eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZFVzdWFyaW8iOiI2Yzc4YWYxYS0yZTdlLTQ5ZDMtYjAxNS1lZDI3YTlmNDgzNWQiLCJub21icmVVc3VhcmlvIjoidGVzdHdlYiIsImlhdCI6MTcwNTA1NzAyMSwiaXNzIjoiaHR0cHM6Ly93d3cuYWR6Z2kuY29tIiwianRpIjoiIn0.J9SasbEaxwU2hlG5YRpDEeEJc8vZgb6cVYzj3cRNo84");
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            // Do something with responseBody
                            string responseBody = objReader.ReadToEnd();
                            Respuesta respuesta = JsonConvert.DeserializeObject<Respuesta>(responseBody);
                            respuesta.Pagination.Links.Actual = url2;
                            return respuesta;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                return null;
            }
        }

        public Respuesta GetProductosPaginados(string parametros)
        {
            string url2 = url + "?offset=0&limit=20";
            if (parametros != "") { url2 = url2 + "&" + parametros; }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url2);
            request.Headers.Add("token:eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZFVzdWFyaW8iOiI2Yzc4YWYxYS0yZTdlLTQ5ZDMtYjAxNS1lZDI3YTlmNDgzNWQiLCJub21icmVVc3VhcmlvIjoidGVzdHdlYiIsImlhdCI6MTcwNTA1NzAyMSwiaXNzIjoiaHR0cHM6Ly93d3cuYWR6Z2kuY29tIiwianRpIjoiIn0.J9SasbEaxwU2hlG5YRpDEeEJc8vZgb6cVYzj3cRNo84");
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            // Do something with responseBody
                            string responseBody = objReader.ReadToEnd();
                            Respuesta respuesta = JsonConvert.DeserializeObject<Respuesta>(responseBody);
                            respuesta.Pagination.Links.Actual = url + "?offset=0&limit=20";
                            return respuesta;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                return null;
            }
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}