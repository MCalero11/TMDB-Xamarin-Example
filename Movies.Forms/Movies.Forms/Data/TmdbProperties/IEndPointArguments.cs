using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Forms.Data.TmdbProperties
{
    public interface IEndPointArguments
    {
        Dictionary<string, string> InitializeArguments();

        Dictionary<string, string> SetArgument(ArgumentTypeEnum option, string value);

        Dictionary<string, string> RemoveArgument(ArgumentTypeEnum option);
        
        Dictionary<string, string> GetArguments();

    }
}
