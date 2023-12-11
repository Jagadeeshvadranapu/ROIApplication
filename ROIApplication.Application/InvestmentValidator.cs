using FluentValidation;
using ROIApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace ROIApplication.Application
{
    public class InvestmentsValidator : AbstractValidator<Model.Investments>
    {
          public InvestmentsValidator()
            {
                RuleForEach(x => x.investment).SetValidator(new InvestmentValidator());
            RuleFor(x => x.investment).Must(coll => coll.Sum(item => item.Percentage) <= 100).WithMessage("Total Percentage should not exceeds 100%");
            RuleFor(x => x.investment).Must(coll => coll.Distinct(new SubEntityComparer()).Count() == coll.Count).WithMessage("duplicate investment options not allowed");
  

        }
        
    }
    public class InvestmentValidator : AbstractValidator<Model.Investment>
    {
        List<string> conditions = new List<string>() { "Cash Investments", "Fixed Interest", "Shares", "Managed Funds", "Exchange Trade Funds", "Investment Bonds", "Annuities", "Listed Investment Companies", "Real Estate Investment Truest" };
        public InvestmentValidator()
        {
            RuleFor(x => x.Amount).NotNull().NotEmpty().GreaterThanOrEqualTo(10000);
            RuleFor(x => x.Percentage).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Option).NotNull().NotEmpty().Must(x => conditions.Contains(x)).WithMessage("Please only use: " + String.Join(", ", conditions));
        }
    }

    public class SubEntityComparer : IEqualityComparer<Investment>
    {
        public bool Equals(Investment x, Investment y)
        {
            if (x == null ^ y == null)
                return false;

            if (ReferenceEquals(x, y))
                return true;

            // your equality comparison logic goes here:
            return x.Option == y.Option && x.Option == y.Option;
        }

        public int GetHashCode(Investment obj)
        {
            return obj.Option.GetHashCode() + 37 * obj.Option.GetHashCode();
        }
    }


}
