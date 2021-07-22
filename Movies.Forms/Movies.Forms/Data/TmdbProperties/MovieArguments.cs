using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Forms.Data.TmdbProperties
{
    public class MovieArguments : IEndPointArguments
    {
        private readonly Dictionary<ArgumentTypeEnum, string> DiscoverQuerys;

        private Dictionary<string, string> Filters = new Dictionary<string, string>();

        public MovieArguments()
        {
            // set the parameters that are accepted by this endpoint
            DiscoverQuerys = new Dictionary<ArgumentTypeEnum, string>();
            DiscoverQuerys.Add(ArgumentTypeEnum.LANGUAJE, "language");
        }
        public Dictionary<string, string> GetArguments()
        {
            return Filters;
        }

        public Dictionary<string, string> InitializeArguments()
        {
            Filters.Add(DiscoverQuerys[ArgumentTypeEnum.LANGUAJE], "en-US");

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
