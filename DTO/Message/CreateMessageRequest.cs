using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Message
{
    public class CreateMessageRequest
    {
        public int HouseNumber { get; set; }
        public string IdentityNumber { get; set; }
        public string Content { get; set; }
    }
}
