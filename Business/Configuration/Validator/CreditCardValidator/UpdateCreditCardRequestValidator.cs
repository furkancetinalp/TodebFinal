using DTO.CreditCard;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.CreditCardValidator
{
    //Validation settings of Creditcard class
    public class UpdateCreditCardRequestValidator:AbstractValidator<UpdateCreditCardRequest>
    {
        public UpdateCreditCardRequestValidator()
        {
            RuleFor(x => x.CardNumber).Length(5).WithMessage("Card number MUST BE 5 digits!!!");
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Name of the card owner cannot be empty!!!");
            RuleFor(x => x.ExpireMonth).InclusiveBetween(1, 12).WithMessage("Please select a valid month (1-12) ");
            RuleFor(x => x.ExpireYear).GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("Please select a valid year!!!");
        }
    }
}
