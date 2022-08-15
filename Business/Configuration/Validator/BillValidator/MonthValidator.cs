using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.BillValidator
{
    //Month validation
    public class MonthValidator: AbstractValidator<int>
    {
        public MonthValidator()
        {
            RuleFor(x => x).InclusiveBetween(1,12).WithMessage("Month value must be in range of 1-12");
        }

    }
}
