using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Forms.Data.RestService
{
    public interface IRestService
    {
        /// <summary>
        /// Gets the json result from the api and returns the content inside the 'result' property
        /// </summary>
        /// <typeparam name="T">Return type</typeparam>
        /// <param name="controller"></param>
        /// <param name="querys"></param>
        /// <returns>Response object with the result</returns>
        Task<Response<T>> GetListAsync<T>(
            string controller, IDictionary<string,string> querys);

        /// <summary>
        /// Get and deserialize an object from api
        /// </summary>
        /// <typeparam name="T">Return type</typeparam>
        /// <param name="controller"></param>
        /// <param name="querys"></param>
        /// <returns>Response object with the result</returns>
        Task<Response<T>> GetAsync<T>(
            string controller, IDictionary<string,string> querys);

        /// <summary>
        /// Send a POST request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="querys"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Response<T>> PostAsync<T>(
            string controller,
            IDictionary<string, string> querys,
            object model);

    }
}
