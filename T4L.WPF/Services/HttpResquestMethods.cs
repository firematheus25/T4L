using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using T4L.Domain;
using T4L.Domain.Entities;

namespace T4L.WPF.Services
{
    public class HttpResquestMethods<T> : Client<T>
    {       

        public HttpResquestMethods()
        {

        }



        public async Task<GenericResult> GetAsync()
        {
            using (var client = new HttpClient())
            {
                var url = new Client<T>().BuilderUri();

                var result = await client.GetAsync(url);
                               
                var json = await result.Content.ReadAsStringAsync();

                                
                var genericResult = JsonConvert.DeserializeObject<GenericResult>(json);

                var data = JsonConvert.DeserializeObject<List<T>>(genericResult.Data.ToString());
                genericResult.Data = data;
                return genericResult;
            }
        }


        public async Task<GenericResult> GetByIdAsync(int id)
        {
            using (var client = new HttpClient())
            {
                var url = new Client<T>().BuilderUri(id);

                var result = await client.GetAsync(url);

                var json = await result.Content.ReadAsStringAsync();


                var genericResult = JsonConvert.DeserializeObject<GenericResult>(json);

                if (genericResult.Data != null)
                {
                    var data = JsonConvert.DeserializeObject<T>(genericResult.Data.ToString());
                    genericResult.Data = data;
                }
                             
                return genericResult;
            }
        }

        public async Task<GenericResult> GetByKeyWordAsync(string keyWord)
        {
            using (var client = new HttpClient())
            {
                var url = new Client<T>().BuilderUri(keyWord.ToUpper());

                var result = await client.GetAsync(url);

                var json = await result.Content.ReadAsStringAsync();


                var genericResult = JsonConvert.DeserializeObject<GenericResult>(json);
                if (genericResult.Data != null)
                {
                    var data = JsonConvert.DeserializeObject<List<T>>(genericResult.Data.ToString());
                    genericResult.Data = data;
                }
              
                return genericResult;
            }
        }

        public async Task<GenericResult> Post(T Objeto)
        {
            using (var client = new HttpClient())
            {
                var url = new Client<T>().BuilderUri();

                var Json = JsonConvert.SerializeObject(Objeto, Formatting.None);

                var Content = new StringContent(Json, Encoding.UTF8, "application/Json");

                var result = await client.PostAsync(url, Content);

                var json = await result.Content.ReadAsStringAsync();


                return JsonConvert.DeserializeObject<GenericResult>(json);
            }
        }

        public async Task<GenericResult> Put(T Objeto)
        {
            using (var client = new HttpClient())
            {
                var url = new Client<T>().BuilderUri();

                var Json = JsonConvert.SerializeObject(Objeto, Formatting.None);

                var Content = new StringContent(Json, Encoding.UTF8, "application/Json");

                var result = await client.PutAsync(url, Content);

                var json = await result.Content.ReadAsStringAsync();


                return JsonConvert.DeserializeObject<GenericResult>(json);
            }
        }


        public async Task<GenericResult> Delete(int Id)
        {
            using (var client = new HttpClient())
            {
                var url = new Client<T>().BuilderUri(Id);

                var result = await client.DeleteAsync(url);

                var json = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<GenericResult>(json);
            }
        }


    }
}
