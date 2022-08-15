using Business.Configuration.Auth;
using Bussines.Configuration.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    //Interface definition and method decleration of login and verification of users
    public interface IAuthService
    {
        CommandResponse VerifyPassword(string email, string password);
        public AccessToken Login(string email, string password);
    }
}
