using DTO.House;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.HouseValidator
{
    //Validator implementation of House class
    public class UpdateHouseRequestValidator: AbstractValidator<UpdateHouseRequest>
    {
        public UpdateHouseRequestValidator()
        {
            RuleFor(x => x.HouseNo).NotEmpty().GreaterThan(0).LessThan(17).WithMessage("House mumber must be greater than 0 and less than 17");
            RuleFor(x => x.FloorNo).NotEmpty().GreaterThanOrEqualTo(0).LessThan(5).WithMessage("Floor number must be greater or equal than 0 and less than 5");
            RuleFor(x => x.HouseBlock).IsInEnum().WithMessage("The value must be 1 or 2  Because 1 => represents Block of 'A' ; 2 => represents Block of 'B'");
            RuleFor(x => x.Type).Must(x => x == "2+1" || x == "3+1").WithMessage("Houses are only '2+1' or '3+1' types!!!");
            RuleFor(x => x.IsOwner).Equals(true || false);
            RuleFor(x => x.IsHouseFilled).Equals(true || false);

        }
    }
}
