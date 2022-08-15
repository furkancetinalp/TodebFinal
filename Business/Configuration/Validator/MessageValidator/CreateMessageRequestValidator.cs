using DTO.Message;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.MessageValidator
{
    public class CreateMessageRequestValidator:AbstractValidator<CreateMessageRequest>
    {
        //Validator implementation for message class
        public CreateMessageRequestValidator()
        {
            RuleFor(x=>x.HouseNumber).NotEmpty().GreaterThan(0);
            RuleFor(x => x.IdentityNumber).Length(4).WithMessage("Please enter 4 digit Identity number of yours!!!");
            RuleFor(x => x.Content).NotEmpty();
        }

    }
}
