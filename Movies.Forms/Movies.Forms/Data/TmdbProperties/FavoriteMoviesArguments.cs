using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Forms.Data.TmdbProperties
{
    public class FavoriteMoviesArguments : IEndPointArguments
    {
        private readonly Dictionary<ArgumentTypeEnum, string> DiscoverQuerys;

        private Dictionary<string, string> Filters = new Dictionary<string, string>();

        public FavoriteMoviesArguments()
        {
            // set the parameters that are accepted by this endpoint
            DiscoverQuerys = new Dictionary<ArgumentTypeEnum, string>();
            DiscoverQuerys.Add(ArgumentTypeEnum.LANGUAJE, "language");
            DiscoverQuerys.Add(ArgumentTypeEnum.SORT_BY, "sort_by");
            DiscoverQuerys.Add(ArgumentTypeEnum.PAGE, "page");
        }
        public Dictionary<string, string> GetArguments()
        {
            return Filters;
        }

        public Dictionary<string, string> InitializeArguments()
        {
            Filters.Add(DiscoverQuerys[ArgumentTypeEnum.LANGUAJE], "en-US");
            Filters.Add(DiscoverQuerys[ArgumentTypeEnum.SORT_BY], "created_at.desc");
            Filters.Add(DiscoverQuerys[ArgumentTypeEnum.PAGE], "1");

            return Filters;
        }

        public Dictionary<string, string> RemoveArgument(ArgumentTypeEnum option)
        {
            string val = string.Empty;
            DiscoverQuerys.TryGetValue(option, out val);

            if (string.IsNullOrEmpty(val))
            {
                return Filters;
            }

            Filters.Remove(val);

            return Filters;
        }

        public Dictionary<string, string> SetArgument(ArgumentTypeEnum option, string value)
        {
            string val = string.Empty;
            DiscoverQuerys.TryGetValue(option, out val);

            if (string.IsNullOrEmpty(val))
            {
                return Filters;
            }

            Filters[val] = value;

            return Filters;
        }
    }
}
