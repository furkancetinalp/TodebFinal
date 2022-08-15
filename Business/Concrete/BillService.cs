using AutoMapper;
using Business.Abstract;
using Business.Configuration.Validator.BillValidator;
using Bussines.Configuration.Extensions;
using Bussines.Configuration.Response;
using DAL.Abstract;
using DTO.Bill;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BillService:IBillService
    {
        private readonly IBillRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        private readonly IHouseRepository _houseRepository;
        private readonly IMapper _mapper;
        public BillService(IBillRepository repository,IUserRepository userRepository,IHouseRepository houseRepository,IMapper mapper, IUserService userService)
        {
            _repository = repository;
            _userRepository = userRepository;
            _houseRepository = houseRepository;

            _mapper = mapper;
            _userService = userService;
        }
        //Monthly assigning all electricity bills all together
        public CommandResponse AssignElectricityInBulk(decimal totalFee, int month)
        {
            var houses = _houseRepository.GetAll(x=>x.IsHouseFilled==true).ToList();
            var count = 0;
            foreach(var entity in houses)
            {
                var check = _repository.Get(x => x.Type == BillType.Electricity && x.HouseNo == entity.Id && x.Date.Month == month);
                if (check is null)
                    count++;
            }
            
            foreach (var entity in houses)
            {
                var check = _repository.Get(x=>x.Type==BillType.Electricity && x.HouseNo==entity.Id && x.Date.Month== month );
                if(check is null)
                {
                    var bill = new CreateBillRequest
                    {
                        HouseNo = entity.HouseNo,
                        Type = BillType.Electricity,
                        Amount = totalFee / count,
                        Month = month
                    };
                    Insert(bill);
                }
            }         
            return new CommandResponse { Message = "Monthly electricity has been assigned successfully", Status = true };
        }
        //Monthly assigning all fee bills all together 
        public CommandResponse AssignFeeInBulk(decimal totalFee, int month)
        {
            var houses = _houseRepository.GetAll(x => x.IsHouseFilled == true).ToList();
            var count = 0;
            foreach (var entity in houses)
            {
                var check = _repository.Get(x => x.Type == BillType.Fee && x.HouseNo == entity.Id && x.Date.Month == month);
                if (check is null)
                    count++;
            }
            foreach (var entity in houses)
            {
                var check = _repository.Get(x => x.Type == BillType.Fee && x.HouseNo == entity.Id && x.Date.Month == month);
                if (check is null)
                {
                    var bill = new CreateBillRequest
                    {
                        HouseNo = entity.HouseNo,
                        Type = BillType.Fee,
                        Amount = totalFee / count,
                        Month = month
                    };
                    Insert(bill);
                }
            }
            return new CommandResponse { Message = "Monthly fee has been assigned successfully", Status = true };
        }
        //Monthly assigning all gas bills all together
        public CommandResponse AssignGasInBulk(decimal totalFee, int month)
        {
            var houses = _houseRepository.GetAll(x => x.IsHouseFilled == true).ToList();
            var count = 0;
            foreach (var entity in houses)
            {
                var check = _repository.Get(x => x.Type == BillType.Gas && x.HouseNo == entity.Id && x.Date.Month == month);
                if (check is null)
                    count++;
            }
            foreach (var entity in houses)
            {
                var check = _repository.Get(x => x.Type == BillType.Gas && x.HouseNo == entity.Id && x.Date.Month == month);
                if (check is null)
                {
                    var bill = new CreateBillRequest
                    {
                        HouseNo = entity.HouseNo,
                        Type = BillType.Gas,
                        Amount = totalFee / count,
                        Month = month
                    };
                    Insert(bill);
                }
            }
            return new CommandResponse { Message = "Monthly gas has been assigned successfully", Status = true };
        }
        //Monthly assigning all water bills all together
        public CommandResponse AssignWaterInBulk(decimal totalFee, int month)
        {
            var houses = _houseRepository.GetAll(x => x.IsHouseFilled == true).ToList();
            var count = 0;
            foreach (var entity in houses)
            {
                var check = _repository.Get(x => x.Type == BillType.Water && x.HouseNo == entity.Id && x.Date.Month == month);
                if (check is null)
                    count++;
            }
            foreach (var entity in houses)
            {
                var check = _repository.Get(x => x.Type == BillType.Water && x.HouseNo == entity.Id && x.Date.Month == month);
                if (check is null)
                {
                    var bill = new CreateBillRequest
                    {
                        HouseNo = entity.HouseNo,
                        Type = BillType.Water,
                        Amount = totalFee / count,
                        Month = month
                    };
                    Insert(bill);
                }
            }
            return new CommandResponse { Message = "Monthly water has been assigned successfully", Status = true };
        }
        //Deleting bills
        public CommandResponse Delete(int houseNo, BillType billType, int month)
        {
            var houseNovalidor = new HouseNoValidator();
            var billTypeValidator = new BillTypeValidator();
            var monthValidator = new MonthValidator();
            houseNovalidor.Validate(houseNo).ThrowIfException();
            billTypeValidator.Validate(billType).ThrowIfException();
            monthValidator.Validate(month).ThrowIfException();
            var house = _houseRepository.Get(x => x.HouseNo == houseNo);
            var entity = _repository.Get(x=>x.Type==billType && x.HouseNo==house.Id && x.Date.Month==month);
            if (entity is null)
            {
                return new CommandResponse { Message = "No data found!", Status = false };
            }
            _repository.Delete(entity);
            _repository.SaveChanges();
            return new CommandResponse { Message = "Bill has been deleted successfully!", Status = true };
        }
        //Getting all bills
        public IEnumerable<GetBillRequest> GetAll()
        {
            var data = _repository.GetAll();
            var mappedData = data.Select(x => _mapper.Map<GetBillRequest>(x)).ToList();
            foreach (var entity in mappedData)
            {
                entity.Owner = _userRepository.Get(x=>x.HouseNo==entity.HouseNo)!=null ? _userRepository.Get(x => x.HouseNo == entity.HouseNo).Name : null;
                //if (user is not null)
                //    entity.Owner = user.Name;
                entity.HouseNo = _houseRepository.Get(x => x.Id == entity.HouseNo).HouseNo;
            }
            return mappedData;
        }
        //Getting list of bills by bill type
        public IEnumerable<GetBillRequest> GetBillsByBillType(BillType billType)
        {
            var data = _repository.GetAll(x => x.Type == billType).ToList();
            var mappedData = data.Select(x => _mapper.Map<GetBillRequest>(x)).ToList();
            foreach (var entity in mappedData)
            {
                entity.Owner = _userRepository.Get(x => x.HouseNo == entity.HouseNo) != null ? _userRepository.Get(x => x.HouseNo == entity.HouseNo).Name : null;
                entity.HouseNo = _houseRepository.Get(x => x.Id == entity.HouseNo).HouseNo;
            }
            return mappedData;
        }
        //Getting list of bills by house number
        public IEnumerable<GetBillRequest> GetBillsByHouse(int HouseNo)
        {
            #region GettingAllBilsByHouseNumber

            var houseNovalidor = new HouseNoValidator();
            houseNovalidor.Validate(HouseNo).ThrowIfException();

            var house = _houseRepository.Get(x => x.HouseNo == HouseNo);
            if(house is null)
                return Enumerable.Empty<GetBillRequest>();

            var bills = _repository.GetAll(x => x.HouseNo == house.Id).ToList();
            var mappedData = bills.Select(x => _mapper.Map<GetBillRequest>(x)).ToList();
            foreach (var entity in mappedData)
            {
                entity.Owner = _userRepository.Get(x => x.HouseNo == entity.HouseNo) != null ? _userRepository.Get(x => x.HouseNo == entity.HouseNo).Name : null;
                entity.HouseNo = _houseRepository.Get(x => x.Id == entity.HouseNo).HouseNo;
            }
            return mappedData;
            #endregion
        }
        //Getting list of bills by specific month
        public IEnumerable<GetBillRequest> GetByBillTypeAndMonth(BillType billType, int month)
        {
            #region Get Specific Type of Bills For a Given Month
            var billTypeValidator = new BillTypeValidator();
            var monthValidator = new MonthValidator();
            billTypeValidator.Validate(billType).ThrowIfException();
            monthValidator.Validate(month).ThrowIfException();

            var data = _repository.GetAll(x => x.Type == billType && x.Date.Month==month).ToList();
            var mappedData = data.Select(x => _mapper.Map<GetBillRequest>(x)).ToList();
            foreach (var entity in mappedData)
            {
                entity.Owner = _userRepository.Get(x => x.HouseNo == entity.HouseNo) != null ? _userRepository.Get(x => x.HouseNo == entity.HouseNo).Name : null;
                entity.HouseNo = _houseRepository.Get(x => x.Id == entity.HouseNo).HouseNo;
            }
            return mappedData;
            #endregion
        }
        //Getting an exact bill by its type, house number and month
        public GetBillRequest GetSpecificBill(int houseNo, BillType billType, int month)
        {
            #region Getting a Single Specific Type of Bill
            var houseNovalidor = new HouseNoValidator();
            var billTypeValidator = new BillTypeValidator();
            var monthValidator = new MonthValidator();
            houseNovalidor.Validate(houseNo).ThrowIfException();
            billTypeValidator.Validate(billType).ThrowIfException();
            monthValidator.Validate(month).ThrowIfException();

            var house = _houseRepository.Get(x => x.HouseNo == houseNo);
            if (house is null)
                return  null;

            var bill = _repository.Get(x => x.HouseNo == house.Id && x.Type==billType && x.Date.Month==month);
            if (bill is null)
                return null;
            var mappedData =  _mapper.Map<GetBillRequest>(bill);

            mappedData.Owner = _userRepository.Get(x => x.HouseNo == mappedData.HouseNo) != null ? _userRepository.Get(x => x.HouseNo == mappedData.HouseNo).Name : null;
            mappedData.HouseNo = _houseRepository.Get(x => x.Id == mappedData.HouseNo).HouseNo;

            return mappedData;
            #endregion
        }
        //Method of bill addition
        public CommandResponse Insert(CreateBillRequest bill)
        {
            var validator = new CreateBillRequestValidator();
            validator.Validate(bill).ThrowIfException();
            var house = _houseRepository.Get(x => x.HouseNo == bill.HouseNo);
            if(house is null)
                return new CommandResponse { Message = "House does not exist!!!", Status = false };
            var entity = _repository.Get(x => x.Type == bill.Type && x.HouseNo == house.Id && x.Date.Month == bill.Month);

            if (entity is not null)
            {
                return new CommandResponse { Message = "Bill already exists! You can try to update via update command", Status = false };
            }
            var mappedData = _mapper.Map<Bill>(bill);
            mappedData.Date = new DateTime(DateTime.Now.Year, bill.Month, DateTime.Now.Day);

            mappedData.HouseNo = house.Id;
            mappedData.UserId = _userRepository.Get(x => x.HouseNo == mappedData.HouseNo) !=null ? _userRepository.Get(x=>x.HouseNo==mappedData.HouseNo).Id : null;
            _repository.Add(mappedData);
            _repository.SaveChanges();
            return new CommandResponse { Message = "Bill has been inserted successfully", Status = true };
        }
        //Monthly total amount of debt from apartment by bill type
        public CommandResponse MonthlyDebtListByBillType(int month, BillType billType)
        {
            var billTypeValidator = new BillTypeValidator();
            var monthValidator = new MonthValidator();
            billTypeValidator.Validate(billType).ThrowIfException();
            monthValidator.Validate(month).ThrowIfException();

            decimal amount = 0;
            var data = GetByBillTypeAndMonth(billType, month);
            foreach (var entity in data)
                amount+=entity.Amount;

            return new CommandResponse { Message = $"Total Debt of Month {month} for {Enum.GetName(billType)}  = {amount} :", Status = true };

        }
    }
}
