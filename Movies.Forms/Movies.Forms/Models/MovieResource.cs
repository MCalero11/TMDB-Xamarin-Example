using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Movies.Forms.Models
{
    public class MovieResource : INotifyPropertyChanged
    {
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Name { get; set; }
        [JsonProperty("release_date")]
        public DateTime? ReleaseDate { get; set; }
        [JsonProperty("vote_average")]
        public double Rate { get; set; }
        [JsonProperty("poster_path")]
        public string Poster { get; set; }
        public string Overview { get; set; }

        private bool _isFavorite;

        public bool IsFavorite
        {
            get { return _isFavorite; }
            set { _isFavorite = value; RaisePropertyChanged("IsFavorite"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string PropertyName)
        {
            var property = PropertyChanged;
            if (property != null)
                property(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
