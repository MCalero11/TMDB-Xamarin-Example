using Movies.Forms.Data;
using Movies.Forms.Data.MockService;
using Movies.Forms.Data.RestService;
using Movies.Forms.Data.TmdbProperties;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Forms.Views.tab_favorites
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmDetailPage : ContentPage
    {
        public FilmDetailPage()
        {
            InitializeComponent();

            BindingContext = 
                new ViewModels.FilmDetailViewModel(new DataService(new RestService()),
                                                   new MovieArguments());
        }
    }
}