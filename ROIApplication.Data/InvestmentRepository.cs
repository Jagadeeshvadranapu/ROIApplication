using ROIApplication.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROIApplication.Data
{
    public class InvestmentRepository : IInvestmentRepository
    {
        public static List<Model.InvestmentOption> lstOptions = new List<Model.InvestmentOption>() {

            new Model.InvestmentOption{ Id = 1, Option="Cash Investments",MinExcepted=8.5,MaxExcepted= 10.00, Fee=0.5},
            new Model.InvestmentOption{ Id = 2, Option="Fixed Interest",MinExcepted=1.00,MaxExcepted= 10.00, Fee=1.00},
            new Model.InvestmentOption{ Id = 3, Option="Shares",MinExcepted=4.30,MaxExcepted= 6.00, Fee=2.50},
            new Model.InvestmentOption{ Id = 4, Option="Managed Funds",MinExcepted=1.00,MaxExcepted= 12.00, Fee=0.30},
            new Model.InvestmentOption{ Id = 5, Option="Exchange Trade Funds",MinExcepted=12.80,MaxExcepted= 25.00, Fee=2.00},
            new Model.InvestmentOption{ Id = 6, Option="Investment Bonds",MinExcepted=1.00,MaxExcepted= 8.00, Fee=0.90},
            new Model.InvestmentOption{ Id = 7, Option="Annuities",MinExcepted=1.00,MaxExcepted= 4.00, Fee=1.40},
            new Model.InvestmentOption{ Id = 8, Option="Listed Investment Companies",MinExcepted=1.00,MaxExcepted= 6.00, Fee=1.30},
            new Model.InvestmentOption{ Id = 9, Option="Real Estate Investment Truest",MinExcepted=1.00,MaxExcepted= 4.00, Fee=2.00}
        };
        public List<Model.InvestmentOption> GetInvestmentOptions() {
            return lstOptions;
        }


    }
}
