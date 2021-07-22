using Movies.Forms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Forms.Data.MockService
{
    public class MockService : IDataService
    {
        private readonly Random Random;
        public MockService()
        {
            Random = new Random();
        }
        public async Task<Response<MovieResource>> GetMovieById(int id, IDictionary<string, string> filters)
        {
            var list = await GetMovieList();

            var movie = list.Result.FirstOrDefault(x => x.Id == id);

            return new Response<MovieResource> { IsSuccess = true, Result =  movie};
        }
        public async Task<Response<List<MovieResource>>> GetMovieList(IDictionary<string, string> filters=null)
        {
            await Task.Delay(1000);
            var list = new List<MovieResource>() {
                new MovieResource {
                    Id = 1,
                    Name = "Name 1",
                    ReleaseDate = DateTime.Now,
                    Rate = Random.Next(1,10),
                    Poster = "/uuvSvLb3ntGA9B0wx2JskVDSuWi.jpg"
                },
                new MovieResource {
                    Id = 2,
                    Name = "Name 2",
                    ReleaseDate = DateTime.Now,
                    Rate = Random.Next(1,10),
                    Poster = "/uuvSvLb3ntGA9B0wx2JskVDSuWi.jpg",
                    Overview = "If you want a float number to have any minimal " +
                    "number of digits before decimal point use N-times zero before " +
                    "decimal point. E.g. pattern „00.0“ formats a float number to string" +
                    " with at least two digits before decimal point and one digit after that."
                },
                new MovieResource {
                    Id = 3,
                    Name = "Name 3 very large very large very large very large very large ",
                    ReleaseDate = DateTime.Now,
                    Rate = Random.Next(1,10),
                    Poster = "/uuvSvLb3ntGA9B0wx2JskVDSuWi.jpg"
                },
                new MovieResource {
                    Id = 4,
                    Name = "Name 4",
                    ReleaseDate = DateTime.Now,
                    Rate = Random.Next(1,10),
                    Poster = "/uuvSvLb3ntGA9B0wx2JskVDSuWi.jpg"
                },

            };

            return new Response<List<MovieResource>> { IsSuccess = true, Result = list };
        }
        public async Task<Response<List<MovieResource>>> GetFavoriteMovieList(IDictionary<string, string> filters=null)
        {
            await Task.Delay(2000);
            var list = new List<MovieResource>() {
                new MovieResource {
                    Id = 3,
                    Name = "Name 3 very large very large very large very large very large ",
                    ReleaseDate = DateTime.Now,
                    Rate = Random.Next(1,10),
                    IsFavorite = true,
                    Poster = "/uuvSvLb3ntGA9B0wx2JskVDSuWi.jpg"
                },
                new MovieResource {
                    Id = 4,
                    Name = "Name 4",
                    ReleaseDate = DateTime.Now,
                    Rate = Random.Next(1,10),
                    IsFavorite = true,
                    Poster = "/uuvSvLb3ntGA9B0wx2JskVDSuWi.jpg"
                },

            };

            return new Response<List<MovieResource>> { IsSuccess = true, Result = list };
        }

        public Task<Response<bool>> MarkMovieAsFavorite(int movieId, bool isFavorite)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> CheckIfMovieIsFavorite(int movieId)
        {
            throw new NotImplementedException();
        }
    }
}
