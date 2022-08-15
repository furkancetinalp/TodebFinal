using DTO.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.UserValidator
{
    public class CreateUserRegisterRequestValidator:AbstractValidator<CreateUserRegisterRequest>
    {
        //Validator implementation for user class
        public CreateUserRegisterRequestValidator()
        {
            RuleFor(x=>x.Mail).NotEmpty().EmailAddress().WithMessage("Empty or invalid E-mail!!!");
            RuleFor(x => x.Phone).NotNull().WithMessage("Phone number is required!!!")
                .Length(11).WithMessage("Length should be 11 character and starts with 05!!! => for example :05345678900")
                .Matches(new Regex(@"^(05(\d{9}))$")).WithMessage("Phone Number not valid");

            RuleFor(x => x.Name).NotNull().WithMessage("Name is required!!!");
            RuleFor(x => x.UserRole).IsInEnum().WithMessage("Role must be either 1 or 2 : 1=>Admin; 2=>User");
            RuleFor(x => x.HouseNo).NotEmpty().WithMessage("House number is required!!!").GreaterThan(0);
            RuleFor(x=>x.IdentityNo).NotEmpty().WithMessage("4 DIGIT identity number is required!!!").Length(4);
            RuleFor(x => x.CarInfo.Length).InclusiveBetween(6, 7)
                .WithMessage("If you dont have a car, please leave it as it is or enter a 7 digit car plate");



        }

    }
}
