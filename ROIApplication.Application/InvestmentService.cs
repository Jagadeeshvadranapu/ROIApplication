using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using ROIApplication.Model;
using FluentValidation.Results;

namespace ROIApplication.Application
{
    //Implement Bussiness Rule / USE CASES
    public class InvestmentService : IInvestmentServices
    {
        private readonly IInvestmentRepository _investmentRepository;
        private static readonly string exchangerateAPI = "https://api.apilayer.com/exchangerates_data";
        private static readonly string exchangerateAPI_Key = "gps2n0OgdRsUE34sQPWdivLYNjWrXDxc";
        public InvestmentService(IInvestmentRepository investmentRepository) {
            this._investmentRepository = investmentRepository;
        }
        List<Model.InvestmentOption> IInvestmentServices.GetInvestmentOptions() {
            List<Model.InvestmentOption> options = new List<Model.InvestmentOption>();
            try
            {
                options = this._investmentRepository.GetInvestmentOptions();

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return options;
        }

        public async Task<double> GetExchangeConvert(double amount, string from, string to)
        {
            RestRequest restRequest;
            RestResponse responseExchange;
            int statusCode;
            double convertedAmount;
            string convertURL = string.Format("/convert?to={0}&from={1}&amount={2}", to, from, amount);
            using (RestClient restClient = new RestClient(exchangerateAPI))
            {
                restRequest = new RestRequest(convertURL, Method.Get);
                restRequest.AddHeader("apikey", exchangerateAPI_Key);
                responseExchange = await restClient.ExecuteAsync(restRequest);
                statusCode = (int)responseExchange.StatusCode;
                if (statusCode == 200)
                {
                    string content = responseExchange.Content;
                    IDictionary<string, object> responseDic = JsonConvert.DeserializeObject<IDictionary<string, object>>(content);
                    convertedAmount = (double)responseDic["result"];
                }
                else
                {
                    throw new HttpRequestException(responseExchange?.ErrorMessage);
                }
            }
            return convertedAmount;

        }
        public async Task<Model.ProjectROI> CalucateProjectedROI(List<Model.Investment> Investment) {
            List<Model.InvestmentOption> options = this._investmentRepository.GetInvestmentOptions();
            Model.InvestmentOption option = new InvestmentOption();
            ProjectROI projectROI = new ProjectROI();
            InvestmentValidator validator = new InvestmentValidator();
            double roi = 0;
            double fee = 0;
            foreach (var investment in Investment)
            {
               ValidationResult validationResult = validator.Validate(investment);
                if (validationResult.IsValid)
                {
                    option = options.Where(x => x.Option.ToLower() == investment.Option.ToLower()).First();
                    projectROI = this.Calucate(investment, option);
                    roi += projectROI.ProjectedROI;
                    fee += projectROI.ProjectedFees;
                }
                else
                {
                    return null;
                }

            }
            double convertedRate = await this.GetExchangeConvert((fee + 250), "USD", "AUD");
            projectROI = new ProjectROI() { ProjectedROI= Math.Round(roi,2), ProjectedFees= Math.Round(convertedRate,2) };
            return projectROI;
        }
        private Model.ProjectROI Calucate(Investment investment, Model.InvestmentOption option) {
            double projectedROI = 0;
            double projectFee = 0;
            switch (option.Option)
            {
                case "Cash Investments":
                    projectedROI += ((investment.Percentage <= 50 ? (investment.Amount * option.MinExcepted) : (investment.Amount * option.MaxExcepted)) / 100);
                    projectFee += ((investment.Percentage <= 50 ? (investment.Amount * option.Fee) : (0)) / 100);
                    break;
                case "Fixed Interest":
                    projectedROI += ((investment.Amount * option.MaxExcepted) / 100);
                    projectFee += ((investment.Amount * option.Fee) / 100);
                    break;
                case "Shares":
                    projectedROI += ((investment.Percentage <= 70 ? (investment.Amount * option.MinExcepted) : (investment.Amount * option.MaxExcepted)) / 100);
                    projectFee += ((investment.Amount * option.Fee) / 100);
                    break;
                case "Managed Funds":
                    projectedROI += ((investment.Amount * option.MaxExcepted) / 100);
                    projectFee += ((investment.Amount * option.Fee) / 100);
                    break;
                case "Exchange Trade Funds":
                    projectedROI += ((investment.Percentage <= 40 ? (investment.Amount * option.MinExcepted) : (investment.Amount * option.MaxExcepted)) / 100);
                    projectFee += ((investment.Amount * option.Fee) / 100);
                    break;
                case "Investment Bonds":
                    projectedROI += ((investment.Amount * option.MaxExcepted) / 100);
                    projectFee += ((investment.Amount * option.Fee) / 100);
                    break;
                case "Annuities":
                    projectedROI += ((investment.Amount * option.MaxExcepted) / 100);
                    projectFee += ((investment.Amount * option.Fee) / 100);
                    break;
                case "Listed Investment Companies":
                    projectedROI += ((investment.Amount * option.MaxExcepted) / 100);
                    projectFee += ((investment.Amount * option.Fee) / 100);
                    break;
                case "Real Estate Investment Truest":
                    projectedROI += ((investment.Amount * option.MaxExcepted) / 100);
                    projectFee += ((investment.Amount * option.Fee) / 100);
                    break;

            }

            ProjectROI projectROI = new ProjectROI() { ProjectedROI = projectedROI, ProjectedFees = projectFee };
            return projectROI;
        }
    }
}
