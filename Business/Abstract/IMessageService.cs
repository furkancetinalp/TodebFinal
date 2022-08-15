using Bussines.Configuration.Response;
using DTO.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    //Interface definition and method decleration of Messages class
    public interface IMessageService
    {
        public CommandResponse Create(CreateMessageRequest request);
        public IEnumerable<GetMessageRequest> GetAll();
        public CommandResponse MarkMessagesAsRead();
        public IEnumerable<GetMessageRequest> UnreadMessages();

    }
}
