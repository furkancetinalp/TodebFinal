using AutoMapper;
using BackgroundJobs.Abstract;
using Business.Abstract;
using Business.Configuration.Auth;
using Business.Configuration.Helper;
using Business.Configuration.Validator.HouseValidator;
using Business.Configuration.Validator.UserValidator;
using Bussines.Configuration.Extensions;
using Bussines.Configuration.Response;
using DAL.Abstract;
using DTO.House;
using DTO.User;
using Microsoft.Extensions.Caching.Distributed;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserService : IUserService
    { 
        private readonly IUserRepository _repository;
        private readonly IHouseRepository _houserepository;
        private readonly IBillRepository _billRepository;

        private readonly IMapper _mapper;
        private readonly IJobs _jobs;

        //Cache
        private readonly IDistributedCache _distributedCache;
        public UserService(IUserRepository repository,IMapper mapper, IHouseRepository houserepository, 
            IBillRepository billRepository,IJobs jobs, IDistributedCache distributedCache)
        {
            _repository = repository;
            _mapper = mapper;
            _houserepository = houserepository;
            _billRepository = billRepository;
            _jobs = jobs;
            _distributedCache = distributedCache;

        }

        //This is the method that should be called first.
        //In this method, an admin will be created and its password will be automatically set
        //It will be returning a password for the admin.
        public CommandResponse CreateAdmin(CreateAdminRequest admin)
        {
            //Validation of input
            var validator = new CreateAdminRequestValidator();
            validator.Validate(admin).ThrowIfException();

            //Checking required conditions
            if (_repository.GetAll().Count() != 0)
                return new CommandResponse { Message = "Admin already exists!!!", Status = false };

            //In order to add an admin, a house should exist in the database.
            //This is the reason why house and admin are created at the same time.
            //That is the only exceptional situation for this application
            var house = new CreateHouseRequest();
            house.HouseNo = 1; house.FloorNo = 1; house.HouseBlock = HouseBlock.A; house.IsHouseFilled = true;
            house.Type = "2+1"; house.IsOwner = true;

            var mappedData = _mapper.Map<House>(house);
            mappedData.FloorNo = house.FloorNo % 4 == 0 ? house.HouseNo / 4 : house.HouseNo / 4 + 1; //setting floor number automatically
            _houserepository.Add(mappedData);
            _houserepository.SaveChanges();

            //Generating a 4 digit random password 
            string GuidKey = Guid.NewGuid().ToString();
            var password = GuidKey.Substring(0, 4);

            //Creating password hash
            byte[] passwordHash, passwordSalt;
            HashHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = _mapper.Map<User>(admin);
            user.HouseNo = mappedData.Id;
            user.UserRole = UserRole.Admin;
            user.UserPassword = new UserPassword()
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            //setting admin permission *******          
            var AdminPermissions = (Permission[])Enum.GetValues(typeof(Permission));

            user.Permissions = AdminPermissions.Select(x => new UserPermission()
            {
                Permission = x
            }).ToList();

            user.CarInfo = admin.CarInfo.Length==6 ? "no car" : admin.CarInfo;
            _repository.Add(user);
            _repository.SaveChanges();


            var key = StringHelper.CreateCacheKey(user.Name, user.Id);
            var cachePermission = System.Text.Json.JsonSerializer.Serialize(AdminPermissions);
            _distributedCache.SetString(key, cachePermission);

            //Returning auto generated password
            return new CommandResponse { Message = $"Your password is: {password}", Status = true };
        }

        public CommandResponse Delete(string identityNumber)
        {
            //Checking required conditions to delete a user.
            var entity = _repository.Get(x=>x.IdentityNo==identityNumber);
            if (entity == null)
            {
                return new CommandResponse { Message = "No user found!", Status = false };
            }

            if(entity.UserRole==UserRole.Admin)
                return new CommandResponse { Message = "Admin account cannot be deleted!!! Change of admin account can only be allowed to be done in the 'Update' section!!!", Status = false };

            //If unpaid bills still exists, that will warn the admin to whether delete bills or
            //make the user pay required bills
            var checkifNotPaidBillsExist = _billRepository.Get(x => x.UserId == entity.Id && x.UserId==entity.Id);
            if(checkifNotPaidBillsExist is not null)        
                return new CommandResponse { Message = "There are unpaid bills found for that user. Please whether delete bills or make that user pay the bills first in order to delete that user.", Status = false };
            

            _repository.Delete(entity);
            _repository.SaveChanges();
            return new CommandResponse { Message = "User has been deleted successfully!", Status = true };
        }
        //Getting all users
        public IEnumerable<GetUserRequest> GetAll()
        {
            var entity= _repository.GetAll();
            var mappedData=entity.Select(x=>_mapper.Map<GetUserRequest>(x)).ToList();
            foreach (var user in mappedData)
                user.HouseNo = _houserepository.Get(x => x.Id == user.HouseNo).HouseNo;

                return mappedData;
        }
        //User by identity number
        public GetUserRequest GetByIdentityNumber(string identityNo)
        {
            var entity = _repository.Get(x => x.IdentityNo == identityNo);
            if (entity is null)
                return null;
            var mappedEntity = _mapper.Map<GetUserRequest>(entity);
            var house = _houserepository.Get(x=>x.Id==entity.HouseNo);
            mappedEntity.HouseNo = house.HouseNo;
            return mappedEntity;


        }
        //User by house number
        public GetUserRequest GetUserByHouseNo(int houseNo)
        {
            var house = _houserepository.Get(x=>x.HouseNo==houseNo);
            if (house is null)
                return new GetUserRequest { Name = "NULL" };
            var entity = _repository.Get(x => x.HouseNo == house.Id);

            var mappedData = _mapper.Map<GetUserRequest>(entity);
            mappedData.HouseNo=house.HouseNo;
            return mappedData;
        }
        //Adding user method
        public CommandResponse Register(CreateUserRegisterRequest register)
        {
            //Required validation
            var validator = new CreateUserRegisterRequestValidator();
            validator.Validate(register).ThrowIfException();     
            //Conditions check
            if(_repository.GetAll().Count() == 0 && register.UserRole!=UserRole.Admin)
                return new CommandResponse { Message = "First user must be admin!! At first, create an admin account!!!", Status = false };

            var checkIfHouseNumberExists = _houserepository.Get(x => x.HouseNo == register.HouseNo);
            if(checkIfHouseNumberExists is null)
                return new CommandResponse { Message = "No house number has been found!!!", Status = false };

            var checkIfHouseIsAssignedToAnotherPerson = _repository.Get(x => x.HouseNo == checkIfHouseNumberExists.Id);
            if(checkIfHouseIsAssignedToAnotherPerson is not null)
                return new CommandResponse { Message = "User for that house number has already been assigned!!!", Status = false };

            var checkIfPersonAlreadyExists = _repository.Get(x=>x.IdentityNo==register.IdentityNo);
            if (checkIfPersonAlreadyExists is not null || _repository.Get(x=>x.Mail==register.Mail)!=null)
            {
                return new CommandResponse { Message = "User already exists!", Status = false };

            }
            var checkifAdminAlreadyExists = _repository.Get(x => x.UserRole==UserRole.Admin);
            if (checkifAdminAlreadyExists is not null && register.UserRole==UserRole.Admin)
                return new CommandResponse { Message = "Admin account exists already! Only 1 admin account is allowed!!!", Status = false };

            // generating 4 digit password automatically
            string GuidKey = Guid.NewGuid().ToString();
            var password = GuidKey.Substring(0,4);

            byte[] passwordHash, passwordSalt;
            HashHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = _mapper.Map<User>(register);
            user.HouseNo = checkIfHouseNumberExists.Id;
            user.UserPassword = new UserPassword()
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            //user permission *******
            var userpermissions = new List<Permission> {Permission.GetSpecificBill,Permission.GetBillByHouseNumber, Permission.GetByBTypeAndMonth,
            Permission.SendMeesageToAdmin,Permission.MakePayment};

            var AdminPermissions = (Permission[])Enum.GetValues(typeof(Permission));

            user.Permissions = userpermissions.Select(x => new UserPermission()
            {
                Permission = x
            }).ToList();

            _repository.Add(user);
            _repository.SaveChanges();


            var key = StringHelper.CreateCacheKey(user.Name, user.Id);
            var cachePermission = System.Text.Json.JsonSerializer.Serialize(userpermissions);
            _distributedCache.SetString(key, cachePermission);

            //THIS IS FOR BACKGROUND JOBS, IT CAN BE USED IN CASE IF NEED ********

            /*
            //Background jobs to be activated:  DELAYEDJOBS
            _jobs.DelayedJob(Convert.ToInt32(register.IdentityNo), register.Name, TimeSpan.FromSeconds(20));

            //Background jobs to be activated:  FIREANDFORGET
            _jobs.FireAndForget(Convert.ToInt32(register.IdentityNo), register.Name);
            */
            return new CommandResponse { Message = $"Your password is: {password}", Status = true };
        }
        //Update of user
        public CommandResponse Update(string identityNumber,UpdateUserRequest user)
        {
            
            var entity = _repository.Get(x=>x.IdentityNo==identityNumber);
            var house = _houserepository.Get(x => x.HouseNo == user.HouseNo);
            if (entity is null || house is null)
                return new CommandResponse { Message = "No user and/or house found!", Status = false };
            var validator = new UpdateUserRequestValidator();
            validator.Validate(user).ThrowIfException();

            if(entity.UserRole==UserRole.Admin && user.UserRole==UserRole.User)
                return new CommandResponse { Message = "This is the only admin account!!! Admin's status cannot be changed!!!", Status = false };

            if (user.UserRole == UserRole.Admin &&entity.UserRole!=UserRole.Admin)
                return new CommandResponse { Message = "Admin already exists!!!", Status = false };
            var ifHouseAlreadyAssignedToSomeone = _repository.Get(x => x.HouseNo == house.Id);
            if(ifHouseAlreadyAssignedToSomeone is not null && ifHouseAlreadyAssignedToSomeone.HouseNo!=entity.HouseNo)
                return new CommandResponse { Message = "You cannot assign a person to that house which is already assigned to another person!!", Status = false };

            /*
            if (user.UserRole == UserRole.Admin)
                _repository.Get(x => x.UserRole == UserRole.Admin).UserRole = UserRole.User;
            */
            var mappedData = _mapper.Map(user, entity);
            mappedData.CarInfo = mappedData.CarInfo.Length == 6 ? "no car" : user.CarInfo;
            _repository.Update(mappedData);
            mappedData.HouseNo = house.Id;
            _repository.SaveChanges();
            return new CommandResponse { Message = "User has been updated successfully", Status = true };

        }
    }
}
