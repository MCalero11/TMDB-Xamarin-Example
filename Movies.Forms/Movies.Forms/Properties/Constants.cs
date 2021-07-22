using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Forms.Properties
{
    public class Constants
    {
        // names for the paths and avoid hardcoded strings
        public const string film_tab_route      = "Film";
        public const string film_details_route  = "FilmDetails";
        public const string fav_tab_route       = "Favorite";
        public const string fav_details_route   = "FavoriteDetails";

        // for rest calls
        public const string ApiVersion      = "3";
        public const string BaseUrl         = @"https://api.themoviedb.org/";
        public const string BaseImageUrl    = @"https://image.tmdb.org/";
    }
}
