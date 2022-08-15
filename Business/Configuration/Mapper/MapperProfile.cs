using AutoMapper;
using DTO.Bill;
using DTO.CreditCard;
using DTO.House;
using DTO.Message;
using DTO.Payment;
using DTO.User;
using Models.Document;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Mapper
{
    //Defining mappings of entities
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateHouseRequest, House>();
            CreateMap<UpdateHouseRequest, House>();
            CreateMap<House, GetHouseRequest>().ForMember(dest => dest.ResidentName, opt => opt.MapFrom(src => src.User.Name));


            CreateMap<CreateUserRegisterRequest, User>();
            CreateMap<User, GetUserRequest>().ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => Enum.GetName(src.UserRole)));
            CreateMap<UpdateUserRequest, User>();


            CreateMap<Bill, GetBillRequest>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.GetName(src.Type)))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.Date.AddDays(15)))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Date.Month));


            CreateMap<CreateBillRequest, Bill>();

            CreateMap<CreateMessageRequest, Message>()
                .ForMember(x=>x.Sender,opt=>opt.MapFrom(src=>src.HouseNumber));

            CreateMap<Message, GetMessageRequest>()
                .ForMember(x => x.SenderHouseNumber, opt => opt.MapFrom(src => src.Sender))
                .ForMember(x=>x.MessageStatus,opt=>opt.MapFrom(src=>Enum.GetName(src.MessageStatus)));


            CreateMap<CreateCreditCardRequest, CreditCard>();
            CreateMap<UpdateCreditCardRequest, CreditCard>();
            CreateMap<CreditCard,GetCreditCardRequest>();


            CreateMap<GetBillRequest, Payment>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.Owner));


            CreateMap<Payment, GetPaymentRecordsRequest>();

            CreateMap<CreateAdminRequest, User>();


        }
    }
}
