using Movies.Forms.Data.Extensions;
using Movies.Forms.Models;
using Movies.Forms.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Forms.Data.RestService
{
    public class RestService : IRestService
    {
        private static HttpClient _httpClient;

        public RestService()
        {
            Initilize();
        }

        /// <summary>
        /// For singleton implementation
        /// </summary>
        private void Initilize()
        {
            if (!(_httpClient is null)) return;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.BaseAddress = new Uri($"{Constants.BaseUrl}");
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.Timeout = TimeSpan.FromSeconds(60);
            _httpClient.MaxResponseContentBufferSize = 10000000;
        }

        public async Task<Response<T>> GetListAsync<T>(string controller, 
            IDictionary<string, string> querys)
        {
            try
            {
                var urlQuerys = string.Empty;
                if (querys != null && querys.Count > 0)
                {
                    urlQuerys = querys.ToQueryString();
                }

                string url = $"{Constants.ApiVersion}/{controller}{urlQuerys}";

                HttpResponseMessage result = await _httpClient.GetAsync(url);
                if (!result.IsSuccessStatusCode)
                {
                    return new Response<T>
                    {
                        Message = result.ReasonPhrase
                    };
                }

                string json = await result.Content.ReadAsStringAsync();

                var objectResult = JObject.Parse(json)["results"].ToObject<T>();

                return new Response<T>
                {
                    IsSuccess = true,
                    Message = result.ReasonPhrase,
                    Result = objectResult,
                };

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                
                return new Response<T>
                {
                    Message = e.Message
                };
            }
        }
        public async Task<Response<T>> GetAsync<T>(string controller, 
            IDictionary<string, string> querys)
        {
            try
            {
                var urlQuerys = string.Empty;
                if (querys != null && querys.Count > 0)
                {
                    urlQuerys = querys.ToQueryString();
                }

                string url = $"{Constants.ApiVersion}/{controller}{urlQuerys}";

                HttpResponseMessage result = await _httpClient.GetAsync(url);
                if (!result.IsSuccessStatusCode)
                {
                    return new Response<T>
                    {
                        Message = result.ReasonPhrase
                    };
                }

                string jsonObject = await result.Content.ReadAsStringAsync();

                return new Response<T>
                {
                    IsSuccess = true,
                    Message = result.ReasonPhrase,
                    Result = JsonConvert.DeserializeObject<T>(jsonObject),
                };

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                
                return new Response<T>
                {
                    Message = e.Message
                };
            }
        }

        public async Task<Response<T>> PostAsync<T>(
            string controller,
            IDictionary<string, string> querys,
            object model)
        {
            try
            {
                var urlQuerys = string.Empty;
                if (querys != null && querys.Count > 0)
                {
                    urlQuerys = querys.ToQueryString();
                }

                string url = $"{Constants.ApiVersion}/{controller}{urlQuerys}";

                string jsonObject = JsonConvert.SerializeObject(model);

                StringContent content = new StringContent(jsonObject,Encoding.UTF8, "application/json");

                HttpResponseMessage result = await _httpClient.PostAsync(url,content);

                if (!result.IsSuccessStatusCode)
                {
                    return new Response<T>
                    {
                        Message = result.ReasonPhrase
                    };
                }
                var stringContent = await result.Content.ReadAsStringAsync();

                T objectResult = default;

                if ((stringContent?.Length ?? 0) > 0 )
                {
                    // convert if needed
                    //objectResult = JsonConvert.DeserializeObject<T>(stringContent);
                }

                return new Response<T>
                {
                    IsSuccess = true,
                    Message = result.ReasonPhrase,
                    Result = objectResult,
                };

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

                return new Response<T>
                {
                    Message = e.Message
                };
            }
        }

    }
}
