using AutoMapper;
using Business.Abstract;
using Business.Configuration.Validator.MessageValidator;
using Bussines.Configuration.Extensions;
using Bussines.Configuration.Response;
using DAL.Abstract;
using DTO.Message;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MessageService: IMessageService
    {
        private readonly IMessageRepository _repository;
        private readonly IUserService _userService;
        private readonly IHouseRepository _houseRepository;
        private readonly IUserRepository _userRepository;

        private IMapper _mapper;
        public MessageService(IMessageRepository repository, IMapper mapper, IUserService userService, IHouseRepository houseRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userService = userService;
            _houseRepository = houseRepository;
            _userRepository = userRepository;           
        }
        //Sending a message to admin
        public CommandResponse Create(CreateMessageRequest request)
        {
            var validator = new CreateMessageRequestValidator();
            validator.Validate(request).ThrowIfException();
            var checkIfHouseExists = _userService.GetUserByHouseNo(request.HouseNumber);
            if (checkIfHouseExists == null)
                return new CommandResponse { Message = "House number is wrong!!!", Status = false };
            if(request.IdentityNumber!=checkIfHouseExists.IdentityNo)
                return new CommandResponse { Message = $"Identity number of the user for houseNO:{request.HouseNumber}  is NOT true!!!", Status = false };

            var message = _mapper.Map<Message>(request);
            message.Receiver = _userRepository.Get(x => x.UserRole == UserRole.Admin).Id;
            message.MessageStatus = MessageStatus.UNREAD;
            message.Date = DateTime.Now;
            _repository.Add(message);
            _repository.SaveChanges();

            return new CommandResponse { Message = "Message has been sent!!!", Status = true };
        }
        //Getting all messages
        public IEnumerable<GetMessageRequest> GetAll()
        {
            var messages = _repository.GetAll();
            var mappedData = messages.Select(x=>_mapper.Map<GetMessageRequest>(x)).ToList();
            return mappedData;
        }
        //Marking read messages as Read
        public CommandResponse MarkMessagesAsRead()
        {
            var messages = _repository.GetAll();
            foreach (var message in messages)
            {
                message.MessageStatus = MessageStatus.READ;
                _repository.Update(message);

            }
            _repository.SaveChanges();
            return new CommandResponse { Message = "Messages are successfully marked as read", Status = true };
        }
        //Getting unread messages
        public IEnumerable<GetMessageRequest> UnreadMessages()
        {
            var messages = _repository.GetAll(x=>x.MessageStatus==MessageStatus.UNREAD);
            var mappedData = messages.Select(x => _mapper.Map<GetMessageRequest>(x));
            return mappedData;

        }
    }
}
