using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.BillValidator
{
    //House number validation
    public class HouseNoValidator:AbstractValidator<int>
    {
        public HouseNoValidator()
        {
            RuleFor(x => x).GreaterThan(0).WithMessage("Invalid house number!!!!");
        }
    }
}
