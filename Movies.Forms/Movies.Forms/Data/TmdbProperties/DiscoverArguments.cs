using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Forms.Data.TmdbProperties
{
    public class DiscoverArguments : IEndPointArguments
    {
        private readonly Dictionary<ArgumentTypeEnum, string> DiscoverQuerys;

        private Dictionary<string, string> Filters = new Dictionary<string, string>();

        public DiscoverArguments()
        {
            // set the parameters that are accepted by this endpoint
            DiscoverQuerys = new Dictionary<ArgumentTypeEnum, string>();
            DiscoverQuerys.Add(ArgumentTypeEnum.LANGUAJE, "language");
            DiscoverQuerys.Add(ArgumentTypeEnum.SORT_BY, "sort_by");
            DiscoverQuerys.Add(ArgumentTypeEnum.INCLUDE_ADULT, "include_adult");
            DiscoverQuerys.Add(ArgumentTypeEnum.INCLUDE_VIDEO, "include_video");
            DiscoverQuerys.Add(ArgumentTypeEnum.PAGE, "page");
            DiscoverQuerys.Add(ArgumentTypeEnum.VOTE_COUNT_GTE, "vote_count.gte");
            DiscoverQuerys.Add(ArgumentTypeEnum.RELEASE_DATE_LTE, "release_date.lte");
        }

        public Dictionary<string, string> InitializeArguments()
        {
            Filters.Add(DiscoverQuerys[ArgumentTypeEnum.LANGUAJE], "en-US");
            Filters.Add(DiscoverQuerys[ArgumentTypeEnum.SORT_BY], "vote_average.desc");
            Filters.Add(DiscoverQuerys[ArgumentTypeEnum.INCLUDE_ADULT], "false");
            Filters.Add(DiscoverQuerys[ArgumentTypeEnum.INCLUDE_VIDEO], "false");
            Filters.Add(DiscoverQuerys[ArgumentTypeEnum.PAGE], "1");
            Filters.Add(DiscoverQuerys[ArgumentTypeEnum.VOTE_COUNT_GTE], "300");
            Filters.Add(DiscoverQuerys[ArgumentTypeEnum.RELEASE_DATE_LTE], DateTime.Now.ToString("yyyy-MM-dd"));

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

        public Dictionary<string, string> GetArguments()
        {
            return Filters;
        }
    }

}
