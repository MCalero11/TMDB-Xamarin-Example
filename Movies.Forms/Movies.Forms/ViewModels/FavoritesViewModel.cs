using Movies.Forms.Data;
using Movies.Forms.Data.Extensions;
using Movies.Forms.Data.TmdbProperties;
using Movies.Forms.Models;
using Movies.Forms.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Movies.Forms.ViewModels
{
    public class FavoritesViewModel : Base.BaseViewModel
    {
        #region Fields
        private readonly IEndPointArguments _endPointArguments;
        private ObservableCollection<MovieResource> _favoriteFilms;
        private MovieResource _selectedFilm;
        private SortOption _sortOptionSelected;
        private ObservableCollection<SortOption> _sortOptions = new ObservableCollection<SortOption>();
        #endregion
        #region Properties
        /// <summary>
        /// Bind ItemsSource from CollectionView
        /// </summary>
        public ObservableCollection<MovieResource> FavoriteFilms
        {
            get { return _favoriteFilms; }
            set { SetProperty(ref _favoriteFilms, value); }
        }
        /// <summary>
        /// Bind SelectedItem from CollectionView
        /// </summary>
        public MovieResource SelectedFilm
        {
            get { return _selectedFilm; }
            set { SetProperty(ref _selectedFilm, value); }
        }
        /// <summary>
        /// Picker option selected
        /// </summary>
        public SortOption SortOptionSelected
        {
            get { return _sortOptionSelected; }
            set
            {
                SetProperty(ref _sortOptionSelected, value);

                SortChangedCommand.Execute(value);
            }
        }
        /// <summary>
        /// Supported sort types
        /// </summary>
        public ObservableCollection<SortOption> SortOptions
        {
            get { return _sortOptions; }
            set { SetProperty(ref _sortOptions, value); }
        }
        #endregion
        #region Commands
        /// <summary>
        /// OnSelectedItem Changes
        /// </summary>
        public ICommand SelectedItemCommand =>
            new Command(async () => await goToDetails());
        /// <summary>
        /// OnSelectedPicker
        /// </summary>
        public ICommand SortChangedCommand =>
            new Command(()=>sortChanged(null));
        #endregion
        public Command<object> FavoriteButtonCommand =>
            new Command<object>(async (obj) => await removeFavorite(obj));
        public FavoritesViewModel(IDataService dataService,
                                  IEndPointArguments endPointArguments) : base(dataService)
        {
            Title = "My Favorites";

            initializeSortOptions();
            _endPointArguments = endPointArguments;
            _endPointArguments.InitializeArguments();

            SortOptionSelected = SortOptions.FirstOrDefault();

            RefreshCommand.Execute(null);
        }
        private async Task removeFavorite(object item)
        {
            if (item is null || !(item is MovieResource)) return;

            var movie = (item as MovieResource);

            _canRefresh = false;
            IsBusy = true;
            var response = await _dataService.MarkMovieAsFavorite(movie.Id, false);
            IsBusy = false;

            if (response?.IsSuccess == true)
            {
                // remove from this tab
                FavoriteFilms.Remove(movie);
            }
            _canRefresh = true;
        }
        protected override async Task refreshData()
        {
            IsBusy = true;
            var response = await _dataService.GetFavoriteMovieList(_endPointArguments.GetArguments());
            IsBusy = false;
            if (response?.IsSuccess == false ||
                response?.Result is null)
            {
                // report error
                return;
            }

            sortChanged(response.Result);
        }
        private void sortChanged(List<MovieResource> list = null)
        {
            if (list is null && (FavoriteFilms?.Count ?? 0) == 0)
            {
                return;
            }

            var listToSort = list ?? FavoriteFilms.ToList();


            FavoriteFilms = 
                new ObservableCollection<MovieResource>(
                    listToSort.SortByOption(SortOptionSelected));
        }

        private void initializeSortOptions()
        {
            SortOptions.Add(new SortOption { Key = "Rating", Value = "vote_average.desc" });
            SortOptions.Add(new SortOption { Key = "Year of release", Value = "release_date.desc" });
            SortOptions.Add(new SortOption { Key = "Name", Value = "original_title.desc" });
        }
        private async Task goToDetails()
        {
            if (SelectedFilm is null) return;

            string route = $"{Constants.fav_tab_route}/{Constants.fav_details_route}";

            await Shell.Current.GoToAsync($"///{route}?filmId={SelectedFilm.Id}");

            SelectedFilm = null;
        }
    }
}
