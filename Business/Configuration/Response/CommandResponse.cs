using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Configuration.Response
{
    //A class that gives an informative message as a result of a request
    public class CommandResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
