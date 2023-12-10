using ROIApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROIApplication.Application
{
    public interface IInvestmentRepository
    {
        List<Model.InvestmentOption> GetInvestmentOptions();
    }
}
