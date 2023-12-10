using ROIApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROIApplication.Application
{
    public interface IInvestmentServices
    {
        //This interface is use for Bussiness Rule / USE CASE
        List<Model.InvestmentOption> GetInvestmentOptions();
        Task<Model.ProjectROI> CalucateProjectedROI(Model.Investments investments);
        Task<double> GetExchangeConvert(double amount, string from, string to);
    }
}
