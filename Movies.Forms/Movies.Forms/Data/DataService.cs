using Movies.Forms.Data.RestService;
using Movies.Forms.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Forms.Data
{
    public class DataService : IDataService
    {
        private readonly IRestService _restService;

        public DataService(IRestService restService)
        {
            _restService = restService;
        }

        public async Task<Response<MovieResource>> GetMovieById(int id,
            IDictionary<string, string> filters)
        {
            Response<MovieResource> response = new Response<MovieResource>();

            var controller = "movie/" + id.ToString();
            injectApiKey(ref filters);

            response = await _restService
                .GetAsync<MovieResource>(controller, filters);

            return response;
        }

        public async Task<Response<List<MovieResource>>> GetMovieList(
            IDictionary<string, string> filters)
        {
            Response<List<MovieResource>> response = new Response<List<MovieResource>>();
            
            injectApiKey(ref filters);
            var controller = "discover/movie";

            response = await _restService
                .GetListAsync<List<MovieResource>>(controller, filters);

            // It seems that the discover controller cannot return 
            // movies marked favorites in the same request

            IDictionary<string, string> querys = new Dictionary<string, string>();
            injectApiKey(ref querys);
            injectSessionId(ref querys);

            if (response.IsSuccess &&
                (response.Result?.Count ?? 0) > 0)
            {
                //for each film send a request to account_request to know if it is marked as a favorite
                foreach (var movie in response.Result)
                {
                    var accountStatesController = $"movie/{movie.Id}/account_states";
                    var result = await _restService
                            .GetAsync<Dictionary<string, string>>(accountStatesController, querys);
                    movie.IsFavorite = checkMovieAccountState(result.Result);
                }
            }

            return response;
        }
        public async Task<Response<List<MovieResource>>> GetFavoriteMovieList(
            IDictionary<string, string> filters)
        {
            Response<List<MovieResource>> response = new Response<List<MovieResource>>();
            
            injectApiKey(ref filters);
            injectSessionId(ref filters);

            var controller = $"account/{Secret.AccountId}/favorite/movies";

            response = await _restService
                .GetListAsync<List<MovieResource>>(controller, filters);

            if ((response.Result?.Count ?? 0) > 0)
            {
                response.Result.ForEach(x => x.IsFavorite = true);
            }

            return response;
        }
        public async Task<Response<bool>> MarkMovieAsFavorite(int movieId, bool isFavorite)
        {
            Response<bool> response = new Response<bool>();

            IDictionary<string, string> querys = new Dictionary<string, string>();

            injectApiKey(ref querys);
            injectSessionId(ref querys);

            var controller = $"account/{Secret.AccountId}/favorite";

            var model =  new { media_type = "movie", media_id = movieId, favorite = isFavorite };

            response = await _restService
                .PostAsync<bool>(controller,querys, model);

            return response;
        }

        public async Task<Response<bool>> CheckIfMovieIsFavorite(int movieId)
        {
            Response<bool> response = new Response<bool>();

            IDictionary<string, string> querys = new Dictionary<string, string>();

            injectApiKey(ref querys);
            injectSessionId(ref querys);

            var controller = $"movie/{movieId}/account_states";

            var result = await _restService
                .GetAsync<Dictionary<string,string>>(controller, querys);

            response.Result = checkMovieAccountState(result.Result);

            return response;
        }

        private bool checkMovieAccountState(Dictionary<string, string> results)
        {
            var isFavorite = false;
            if ((results?.Count ?? 0) > 0 &&
                results.ContainsKey("favorite"))
            {
                var fav = false;
                bool.TryParse(results["favorite"], out fav);

                isFavorite = fav;
            }

            return isFavorite;
        }

        private void injectApiKey(ref IDictionary<string, string> query)
        {
            if (query is null)
            {
                query = new Dictionary<string, string>();
            }

            query["api_key"] = Secret.MyApiKey;
        }
        private void injectSessionId(ref IDictionary<string, string> query)
        {
            if (query is null)
            {
                query = new Dictionary<string, string>();
            }

            query["session_id"] = Secret.SesionId;
        }

        
    }


}
