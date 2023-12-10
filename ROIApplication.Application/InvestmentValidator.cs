using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ROIApplication.Application
{
    public class InvestmentValidator : AbstractValidator<Model.Investment>
    {
        public InvestmentValidator() {
            RuleFor(x => x.Amount).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
