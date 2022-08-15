using AutoMapper;
using Business.Abstract;
using Business.Configuration.Validator.CreditCardValidator;
using Bussines.Configuration.Extensions;
using Bussines.Configuration.Response;
using DAL.Abstract;
using DTO.CreditCard;
using Models.Document;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CreditCardService: ICreditCardService
    {
        private readonly ICreditCardRepository _repository;
        private readonly IMapper _mapper;
        public CreditCardService(ICreditCardRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;   
        }
        //Adding a credit card in MongoDb
        public CommandResponse Add(CreateCreditCardRequest model)
        {
            var validator = new CreateCreditCardRequestValidator();
            validator.Validate(model).ThrowIfException();
            
            var data =_repository.Get(x=>x.CardNumber==model.CardNumber);
            if (data is not null)
                return new CommandResponse { Message = "Card exists already!!!", Status = false };

            var mappedData = _mapper.Map<CreditCard>(model);
            mappedData.Balance = 1000;//Balance is set to 1000 at initial
            _repository.Add(mappedData);

            return new CommandResponse { Message = "Card has been added successfully!!!", Status = true };
        }
        //Updating credit card if conditions are met
        public CommandResponse Update(string cardNumber,UpdateCreditCardRequest model)
        {
            var validator = new UpdateCreditCardRequestValidator();
            validator.Validate(model).ThrowIfException();
            var data = _repository.Get(x => x.CardNumber == cardNumber);
            if (data is  null)
                return new CommandResponse { Message = "Card does not exist in the database!!!", Status = false };
            var mappedData = _mapper.Map(model,data);
            _repository.Update(mappedData);

            return new CommandResponse { Message = "Card has been updated successfully!!!", Status = true };


        }
        //Deleting credit card from MongoDb
        public CommandResponse Delete(string CardNumber)
        {           
            var validator = new CardNumberValidator();
            validator.Validate(CardNumber).ThrowIfException();
            
            var data = _repository.Get(x=>x.CardNumber== CardNumber);
            if (data is null)
                return new CommandResponse { Message = "Card does not exist in the database!!!", Status = false };

            _repository.Delete(data.Id);
            return new CommandResponse { Message = "Card has been updated successfully!!!", Status = true };
        }
        //Getting credit card by card number
        public GetCreditCardRequest Get(string CardNumber)
        {

            var validator = new CardNumberValidator();
            validator.Validate(CardNumber).ThrowIfException();

            var data = _repository.Get(x => x.CardNumber == CardNumber);
            if (data is null)
                return null;
            
            var mappedData = _mapper.Map<GetCreditCardRequest>(data);

            return mappedData;
        }
        //A method to show errors to the user in a certain format
        public void TestExceptionFilter()
        {
            throw new Exception("Exception!!!");
        }

        /*
        public IEnumerable<GetCreditCardRequest> GetAll()
        {
            return _repository.GetAll();
        }
        */

    }

}
