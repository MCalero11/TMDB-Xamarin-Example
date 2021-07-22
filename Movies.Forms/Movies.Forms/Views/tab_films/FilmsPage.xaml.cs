using Movies.Forms.Data;
using Movies.Forms.Data.RestService;
using Movies.Forms.Data.TmdbProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Forms.Views.tab_films
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmsPage : ContentPage
    {
        public FilmsPage()
        {
            InitializeComponent();
            
            BindingContext = 
                new ViewModels.FilmsViewModel(new DataService(new RestService()), 
                                              new DiscoverArguments());
        }
    }
}