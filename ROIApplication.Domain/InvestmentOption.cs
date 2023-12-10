using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROIApplication.Model
{
    public class InvestmentOption
    {
        public int Id { get; set; }
        public string Option { get; set; }
        public double MinExcepted { get; set; }
        public double MaxExcepted { get; set; }
        public double Fee { get; set; }
       
    }
    public class Investment {
        public double Amount { get; set; }
        public string Option { get; set; }
        public double Percentage { get; set;}
    }
    public class ProjectROI { 
       public double ProjectedROI { get; set; }
       public double ProjectedFees { get; set; }
    }

    public class Investments { 
        public List<Model.Investment> investment { get; set; }
    }
}
