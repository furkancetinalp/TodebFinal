using AutoMapper;
using Business.Abstract;
using Business.Configuration.Validator.HouseValidator;
using Bussines.Configuration.Extensions;
using Bussines.Configuration.Response;
using DAL.Abstract;
using DTO.House;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class HouseService: IHouseService
    {
        private readonly IHouseRepository _repository;
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;
        public HouseService(IHouseRepository repository,IMapper mapper, IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        //Deleting house info
        public CommandResponse Delete(int houseNo)
        {
            var house = _repository.Get(x => x.HouseNo == houseNo);
            if(house is null)
            {
                return new CommandResponse { Message = "No data found!", Status = false };
            }
            _repository.Delete(house);
            _repository.SaveChanges();
            return new CommandResponse { Message = "House has been deleted successfully!", Status = true };
        }
        //Getting house by its number
        public GetHouseRequest Get(int houseNo)
        {
            var house = _repository.Get(x=>x.HouseNo==houseNo);
            if (house is null)
                return null;
            var mappedData = _mapper.Map<GetHouseRequest>(house);
            mappedData.ResidentName = _userRepository.Get(x => x.HouseNo == house.Id)==null?null: _userRepository.Get(x => x.HouseNo == house.Id).Name;
            
            return mappedData;
        }
        //Getting all house data
        public IEnumerable<GetHouseRequest> GetAll()
        {
            var data = _repository.GetAll();
            var mappedData = data.Select(x => _mapper.Map<GetHouseRequest>(x)).ToList();
            foreach (var house in mappedData)
                house.ResidentName = Get(house.HouseNo)==null?null: Get(house.HouseNo).ResidentName;
            
            return mappedData;  
        }
        //Adding house value inside of houses class
        public CommandResponse Insert(CreateHouseRequest house)
        {
            var validator = new CreateHouseRequestValidator();
            validator.Validate(house).ThrowIfException();
            var data = _repository.Get(x=>x.HouseNo==house.HouseNo);
            if (data is not null)
            {
                return new CommandResponse { Message = "House already exists!", Status = false };
            }
            var mappedData = _mapper.Map<House>(house);
            mappedData.FloorNo=house.HouseNo%4==0?house.HouseNo/4:house.HouseNo/4+1; //setting floor number automatically
            _repository.Add(mappedData);
            _repository.SaveChanges();
            return new CommandResponse { Message = "House has been added successfully", Status = true };
        }
        //Updating house
        public CommandResponse Update(int houseNo, UpdateHouseRequest house)
        {
            var validator = new UpdateHouseRequestValidator();
            validator.Validate(house).ThrowIfException();
            var entity = _repository.Get(x=>x.HouseNo==houseNo);
            if (entity is null)
            {
                return new CommandResponse { Message = "No data found!", Status = false };
            }
            var mappedData = _mapper.Map(house, entity);
            mappedData.FloorNo = house.HouseNo % 4 == 0 ? house.HouseNo / 4 : house.HouseNo / 4 + 1; //setting floor number automatically
            _repository.Update(mappedData);
            _repository.SaveChanges();
            return new CommandResponse { Message = "House has been updated successfully", Status = true };
        }
    }
}
