using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Helper
{
    //For Redis, cache symbol of records
    public static class StringHelper
    {
        public static string CreateCacheKey(string userName, int userId)
        {
            return string.Concat(userName.Substring(0, 3), "_", userId);
        }
    }
}
