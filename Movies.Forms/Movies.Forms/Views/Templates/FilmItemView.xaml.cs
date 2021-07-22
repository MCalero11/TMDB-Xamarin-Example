using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Forms.Views.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmItemView : Grid
    {
        public FilmItemView()
        {
            InitializeComponent();
        }


    }
}