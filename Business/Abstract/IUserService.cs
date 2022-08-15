using Bussines.Configuration.Response;
using DTO.House;
using DTO.User;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    //Interface definition and method decleration of Users class
    public interface IUserService
    {
        public CommandResponse CreateAdmin(CreateAdminRequest admin);
        public CommandResponse Register(CreateUserRegisterRequest register);
        public IEnumerable<GetUserRequest> GetAll();
        public GetUserRequest GetByIdentityNumber(string identityNo);
        public CommandResponse Update(string identityNumber,UpdateUserRequest user);
        public CommandResponse Delete(string identityNumber);
        public GetUserRequest GetUserByHouseNo(int houseNo);

    }
}
