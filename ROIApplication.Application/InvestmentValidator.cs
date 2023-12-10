using FluentValidation;

namespace ROIApplication.Application
{
    public class InvestmentsValidator : AbstractValidator<Model.Investments>
    {
          public InvestmentsValidator()
            {
                RuleForEach(x => x.investment).SetValidator(new InvestmentValidator());
            }
        
    }
    public class InvestmentValidator : AbstractValidator<Model.Investment>
    {
        public InvestmentValidator()
        {
            RuleFor(x => x.Amount).NotNull().NotEmpty().GreaterThanOrEqualTo(10000);
            RuleFor(x => x.Percentage).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Option).NotNull().NotEmpty();
        }

    }


}
