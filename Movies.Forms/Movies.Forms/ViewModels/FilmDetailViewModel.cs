using Movies.Forms.Data;
using Movies.Forms.Data.TmdbProperties;
using Movies.Forms.Models;
using Movies.Forms.Properties;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Movies.Forms.ViewModels
{
    [QueryProperty("FilmId", "filmId")]
    public class FilmDetailViewModel : Base.BaseViewModel
    {
        #region Fields
        private readonly IEndPointArguments _endPointArguments;
        private string filmId;
        private string _titleFilm;
        private string _synopsis;
        private string _poster;
        private string _rating;
        private DateTime _release;
        private bool _isFav;
        private IDictionary<string, string> filters = new Dictionary<string, string>();
        #endregion
        #region Properties
        /// <summary>
        /// Xamarin Shell fills this OnNavigation
        /// </summary>
        public string FilmId
        {
            get { return filmId; }
            set
            {
                if (value != null && value != filmId)
                {
                    filmId = Uri.UnescapeDataString(value);
                    
                    RefreshCommand.Execute(null);
                }
            }
        }
        public string TitleFilm
        {
            get { return _titleFilm; }
            set { SetProperty(ref _titleFilm, value); }
        }
        public string Synopsis
        {
            get { return _synopsis; }
            set { SetProperty(ref _synopsis, value); }
        }
        public string Poster
        {
            get { return _poster; }
            set { SetProperty(ref _poster, value); }
        }
        public string Rating
        {
            get { return _rating; }
            set { SetProperty(ref _rating, value); }
        }
        public DateTime Release
        {
            get { return _release; }
            set { SetProperty(ref _release, value); }
        }
        public bool IsFav
        {
            get { return _isFav; }
            set { SetProperty(ref _isFav, value); }
        }
        #endregion

        public ICommand FavoriteButtonCommand =>
            new Command(async () => await addOrRemoveFavorite());
        public FilmDetailViewModel(IDataService dataService,
                                  IEndPointArguments endPointArguments) : base(dataService)
        {
            Title = "More info";


            _endPointArguments = endPointArguments;
        }
        private async Task addOrRemoveFavorite()
        {
            int id = 0;
            int.TryParse(FilmId, out id);

            IsBusy = true;
            var response = await _dataService.MarkMovieAsFavorite(id, !IsFav);
            IsBusy = true;

            if (response?.IsSuccess == true)
            {
                IsFav = !IsFav;
            }
        }
        protected override async Task refreshData()
        {
            int id = 0;
            int.TryParse(FilmId, out id);

            if (id == 0)
            {
                await Shell.Current.GoToAsync("..");
                return;
            }
            _canRefresh = false;
            IsBusy = true;
            var response = await _dataService.GetMovieById(id,filters);
            var markAsFavorite = await _dataService.CheckIfMovieIsFavorite(id);
            IsBusy = false;
            _canRefresh = true;
            if (response?.IsSuccess == false || response.Result is null)
            {
                await Shell.Current.GoToAsync("..");
                return;
            }

            var film = response.Result;

            TitleFilm = film.Name;
            Synopsis = film.Overview;
            Poster = film.Poster;
            Rating = film.Rate.ToString("0.0");
            Release = film.ReleaseDate ?? DateTime.Now;
            IsFav = markAsFavorite.Result;
        }

    }
}
