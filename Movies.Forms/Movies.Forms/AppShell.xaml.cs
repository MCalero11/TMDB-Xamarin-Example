using Movies.Forms.Properties;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(
                $"{Constants.film_tab_route}/{Constants.film_details_route}", 
                typeof(Views.tab_films.FilmDetailPage));

            Routing.RegisterRoute(
                $"{Constants.fav_tab_route}/{Constants.fav_details_route}", 
                typeof(Views.tab_favorites.FilmDetailPage));

        }
    }
}