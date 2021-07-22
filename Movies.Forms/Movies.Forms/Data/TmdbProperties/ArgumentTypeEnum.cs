using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Forms.Data.TmdbProperties
{
    [Flags]
    public enum ArgumentTypeEnum
    {
        LANGUAJE,
        SORT_BY,
        INCLUDE_ADULT,
        INCLUDE_VIDEO,
        PAGE,
        VOTE_COUNT_GTE,
        RELEASE_DATE_LTE,
    }
}
