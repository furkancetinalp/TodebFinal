using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Message
{
    public class GetMessageRequest
    {
        public int SenderHouseNumber { get; set; }
        public string Content { get; set; }
        public string MessageStatus { get; set; }
        public DateTime Date { get; set; }

    }
}
