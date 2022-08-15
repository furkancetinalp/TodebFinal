using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Auth
{
    //A class to return a token and its expiration time.
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
