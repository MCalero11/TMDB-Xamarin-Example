using Movies.Forms.Data;
using Movies.Forms.Data.MockService;
using Movies.Forms.Data.RestService;
using Movies.Forms.Data.TmdbProperties;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Forms.Views.tab_favorites
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesPage : ContentPage
    {
        public FavoritesPage()
        {
            InitializeComponent();
            BindingContext = 
                new ViewModels.FavoritesViewModel(new DataService(new RestService()),
                                                  new FavoriteMoviesArguments());
        }
    }
}