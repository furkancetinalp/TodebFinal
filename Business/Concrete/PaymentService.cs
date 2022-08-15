using AutoMapper;
using BackgroundJobs.Abstract;
using Business.Abstract;
using Business.Configuration.Validator.BillValidator;
using Bussines.Configuration.Extensions;
using Bussines.Configuration.Response;
using DAL.Abstract;
using DAL.Concrete.EF;
using DTO.Bill;
using DTO.Payment;
using Models.Document;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PaymentService: IPaymentService
    {
        private readonly IPaymentRepository _repository;
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IHouseRepository _houseRepository;

        private readonly IBillService _billService;
        private readonly IBillRepository _billRepository;

        public IMapper _mapper;
        public PaymentService(IPaymentRepository repository, IBillService billService,IMapper mapper,
            ICreditCardRepository creditCardRepository,IBillRepository billRepository,
            IHouseRepository houseRepository)
        {
            _repository = repository;
            _billService = billService;
            _mapper = mapper;
            _creditCardRepository = creditCardRepository;
            _billRepository = billRepository;
            _houseRepository = houseRepository;
        }
        //Method for Paymenf of bills
        public CommandResponse Add(int houseNo, BillType billType, int month,string CardNumber)
        {
            var bill = _billService.GetSpecificBill(houseNo, billType, month);
            var house = _houseRepository.Get(x => x.HouseNo == houseNo);

            if (bill is null)
                return new CommandResponse { Message = "Bill could not be found!!!", Status = false };

            var data = _billRepository.Get(x => x.HouseNo == house.Id && x.Type == billType && x.Date.Month == month);

            if (data.IsPaymentMade==true)
                return new CommandResponse { Message = "Bill has been paid already!!!", Status = false };

            var checkifCreditCardExists = _creditCardRepository.Get(x=>x.CardNumber==CardNumber);
            if (checkifCreditCardExists is null)
                return new CommandResponse { Message = "Credit card could not be found!!! Please add a credit card and then try to pay the bill", Status = false };

            if (checkifCreditCardExists.Balance < data.Amount)
                return new CommandResponse { Message = "Insufficient Balance to pay the bill!!! ", Status = false };

            checkifCreditCardExists.Balance-=data.Amount;

            //data.IsPaymentMade = true;

            _creditCardRepository.Update(checkifCreditCardExists);
            
            _billRepository.Delete(data);
            _billRepository.SaveChanges();

            var mappedData = _mapper.Map<Payment>(bill);
            mappedData.PaymentDate = DateTime.UtcNow;

            //Adding bill to MongoDb 
            _repository.Add(mappedData);
            return new CommandResponse { Message = "Payment of bill has been made successfully", Status = true };
        }
        //Getting payment records by house number
        public GetPaymentRecordsRequest Get(int HouseNo)
        {
            var validator = new MonthValidator();
            validator.Validate(HouseNo).ThrowIfException();

            var data = _repository.Get(x => x.HouseNo == HouseNo);
            if (data is null)
                return null;
            var mappedData = _mapper.Map<GetPaymentRecordsRequest>(data);
            return mappedData;


        }
        //Getting all payment records
        public IEnumerable<GetPaymentRecordsRequest> GetAll()
        {
            var data = _repository.GetAll();
            if (data is null)
                return null;
            var mappedData = data.Select(x=>_mapper.Map<GetPaymentRecordsRequest>(x));
            return mappedData;
        }
    }
}
