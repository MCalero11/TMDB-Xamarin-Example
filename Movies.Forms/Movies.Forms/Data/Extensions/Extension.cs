using Movies.Forms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movies.Forms.Data.Extensions
{
    public static partial class Extension
    {
        /// <summary>
        /// Converts a key value pair to a qualified url argument
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns>String in key value pair format</returns>
        public static string ToQueryString(this IDictionary<string, string> dictionary)
        {
            return '?' + string.Join("&", dictionary.Select(p => p.Key + '=' + Uri.EscapeUriString(p.Value)).ToArray());
        }
        /// <summary>
        /// Sort a list of movies
        /// </summary>
        /// <param name="movies"></param>
        /// <param name="option"></param>
        /// <returns>A collection sorted</returns>
        public static IList<MovieResource> SortByOption(this IList<MovieResource> movies, SortOption option)
        {
            movies = movies ?? new List<MovieResource>();
            option = option ?? new SortOption();

            switch (option.Value)
            {
                case "release_date.desc":
                    movies = movies.OrderByDescending(x => x.ReleaseDate).ToList();
                    break;
                case "original_title.desc":
                    movies = movies.OrderBy(x => x.Name).ToList();
                    break;
                default:
                    movies = movies.OrderByDescending(x => x.Rate).ToList();
                    break;
            }

            return movies;
        }

    }
}
