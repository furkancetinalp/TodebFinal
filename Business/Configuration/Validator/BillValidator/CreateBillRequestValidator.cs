using DTO.Bill;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.BillValidator
{
    //Validation of bill entity
    public class CreateBillRequestValidator:AbstractValidator<CreateBillRequest>
    {
        public CreateBillRequestValidator()
        {
            RuleFor(x => x.HouseNo).GreaterThan(0).WithMessage("Invalid house name!!!");
            RuleFor(x => x.Month).InclusiveBetween(DateTime.Now.Month-1, DateTime.Now.Month).WithMessage("Month must be in range of [previous month and-current month]");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0");
            RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid bill type!!! Select: 1=>Fee; 2=>Electricity; 3=>Water; 4=>Gas");

        }

    }
}
