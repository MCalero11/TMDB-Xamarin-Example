using Movies.Forms.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Forms.Data
{
    public interface IDataService
    {
        /// <summary>
        /// Get list of most relevant movies
        /// </summary>
        /// <param name="filters">Filters for api request</param>
        /// <returns>A object Response with list of movies</returns>
        Task<Response<List<MovieResource>>> GetMovieList(IDictionary<string,string> filters);
        /// <summary>
        /// Get list of favorite movies
        /// </summary>
        /// <param name="filters">Filters for api request</param>
        /// <returns>A object Response with list of movies</returns>
        Task<Response<List<MovieResource>>> GetFavoriteMovieList(IDictionary<string,string> filters);
        /// <summary>
        /// Get more information about a movie
        /// </summary>
        /// <param name="id">Movie ID</param>
        /// <param name="filters">Filters for api request</param>
        /// <returns>Response object with Movie result</returns>
        Task<Response<MovieResource>> GetMovieById(int id, IDictionary<string, string> filters);
        /// <summary>
        /// Check or Uncheck a movie as favorite
        /// </summary>
        /// <param name="movieId">Movie ID</param>
        /// <param name="isFavorite">Specify if add or remove movie of favorite list</param>
        /// <returns></returns>
        Task<Response<bool>> MarkMovieAsFavorite(int movieId, bool isFavorite);
        /// <summary>
        /// Checks if a movie belongs to favorite list
        /// </summary>
        /// <param name="movieId">Movie ID</param>
        /// <returns></returns>
        Task<Response<bool>> CheckIfMovieIsFavorite(int movieId);

    }
}
