using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Forms.Data
{
    public class Response<T>
    {
        /// <summary>
        /// Indicates if the api request return a 2xx code
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Operation rfesult message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Return object
        /// </summary>
        public T Result { get; set; }
    }
}
