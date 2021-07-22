using Movies.Forms.Data;
using Movies.Forms.Data.Extensions;
using Movies.Forms.Data.TmdbProperties;
using Movies.Forms.Models;
using Movies.Forms.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Movies.Forms.ViewModels
{
    public class FilmsViewModel : Base.BaseViewModel
    {
        #region Fields
        private readonly IEndPointArguments _endPointArguments;
        private ObservableCollection<MovieResource> _films;
        private MovieResource _selectedFilm;
        private SortOption _sortOptionSelected;
        private ObservableCollection<SortOption> _sortOptions = new ObservableCollection<SortOption>();
        #endregion
        #region Properties
        /// <summary>
        /// Bind ItemsSource property from CollectionView
        /// </summary>
        public ObservableCollection<MovieResource> Films
        {
            get { return _films; }
            set { SetProperty(ref _films, value); }
        }
        /// <summary>
        /// Bind SelectedItem property from CollectionView
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
        public Command<object> SortChangedCommand =>
            new Command<object>(async (obj) => await sortChanged(obj));
        public Command<object> FavoriteButtonCommand =>
            new Command<object>(async (obj) => await addOrRemoveFavorite(obj));

        #endregion
        
        public FilmsViewModel(IDataService dataService,
                              IEndPointArguments endPointArguments) : base(dataService)
        {
            Title = "New films";

            initializeSortOptions();
            _endPointArguments = endPointArguments;
            _endPointArguments.InitializeArguments();

            SortOptionSelected = SortOptions.FirstOrDefault();
        }
        
        private async Task sortChanged(object option)
        {
            if (option is null || !(option is SortOption))
            {
                return;
            }

            // change the value of sort_by before calling the api
            _endPointArguments.SetArgument(ArgumentTypeEnum.SORT_BY,
                (option as SortOption).Value);

            await refreshData();
        }
        protected override async Task refreshData()
        {
            IsBusy = true;
            var response = await _dataService.GetMovieList(_endPointArguments.GetArguments());
            IsBusy = false;

            if (response?.IsSuccess == false ||
                response?.Result is null)
            {
                // show error message
                return;
            }

            Films = new ObservableCollection<MovieResource>(
                response.Result.SortByOption(SortOptionSelected));
        }

        private void initializeSortOptions()
        {
            SortOptions.Add(new SortOption { Key = "Rating", Value = "vote_average.desc" });
            SortOptions.Add(new SortOption { Key = "Year of release", Value = "release_date.desc" });
            SortOptions.Add(new SortOption { Key = "Name", Value = "original_title.desc" });
        }
        private async Task addOrRemoveFavorite(object item)
        {
            if (item is null || !(item is MovieResource)) return;

            var movie = (item as MovieResource);

            _canRefresh = false;
            IsBusy = true;
            var response = await _dataService.MarkMovieAsFavorite(movie.Id, !movie.IsFavorite);
            IsBusy = false;
            _canRefresh = true;

            if (response?.IsSuccess == true)
            {
                movie.IsFavorite = !movie.IsFavorite;

            }
            
        }

        private async Task goToDetails()
        {
            if (SelectedFilm is null) return;

            string route = $"{Constants.film_tab_route}/{Constants.film_details_route}";

            await Shell.Current.GoToAsync($"///{route}?filmId={SelectedFilm.Id}");

            SelectedFilm = null;
        }
    }
}
