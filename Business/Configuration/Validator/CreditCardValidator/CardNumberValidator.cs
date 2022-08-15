using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.CreditCardValidator
{
    //SYMBOLICALLY card length is set to 5 digits
    public class CardNumberValidator:AbstractValidator<string>
    {
        public CardNumberValidator()
        {
            RuleFor(x => x).Length(5).WithMessage("Please enter a valid 5 digit credit card number!!!");

        }
    }
}
