using FluentValidation;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.BillValidator
{
    //Bill type validation
    public class BillTypeValidator: AbstractValidator<BillType>
    {
        public BillTypeValidator()
        {
            RuleFor(x => x).IsInEnum().WithMessage("INVALID selection!!! Choose 1=>'Fee' , 2=>'Electricity', 3=>'Water' , 4=>'Gas' ");
        }
    }
}
