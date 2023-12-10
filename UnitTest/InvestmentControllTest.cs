using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ROIApplication.API.Controllers;
using ROIApplication.Application;
using ROIApplication.Data;
using ROIApplication.Model;
using System.Globalization;

namespace UnitTest
{
    public class InvestmentControllTest
    {
        InvestmentController _controll;
        IInvestmentServices _service;
        IInvestmentRepository _repository;

        public InvestmentControllTest() {
            _repository = new InvestmentRepository();
            _service = new IInvestmentService(_repository);
            _controll = new InvestmentController(_service);
        }

        [Fact]
        public void GetInvestmentOptions_Success()
        {
            //arrage

            //act
            var result =  _controll.GetInvestmentOptions();
            var resultType = result.Result as OkObjectResult;
            var resultList = resultType.Value as List<InvestmentOption>;
            
            //assert
            Assert.NotNull(resultList);
            Assert.IsType<List<InvestmentOption>>(resultType.Value);
            Assert.Equal(9,resultList.Count);
        }
        [Fact]
        public void GetExchangeConvert_Success()
        {
            //arrage
            int amout = 1000;
            string from = "USD";
            string to = "AUD";
            //act
            var result = _controll.GetExchangeConvert(amout,from,to);
            var resultType = result.Result as OkObjectResult;
            var resultList = resultType.Value;

            //assert
            Assert.NotNull(resultList);
            Assert.IsType<double>(resultType.Value);
            Assert.Equal(StatusCodes.Status200OK, resultType.StatusCode);
        }
        [Fact]
        public void CalucateProjectedROI_Success()
        {
            //arrage
            List<Investment> investment = new List<Investment>() { 
                new Investment{ Amount = 100000.00, Option = "Cash Investments",Percentage = 80}
            };
           Investments investments = new Investments() { investment = investment};
            //act
            var result = _controll.CalucateProjectedROI(investments);
            var resultType = result.Result as OkObjectResult;
            var resultList = resultType.Value;

            //assert
            Assert.NotNull(resultList);
            Assert.IsType<ProjectROI>(resultType.Value);
            Assert.Equal(StatusCodes.Status200OK, resultType.StatusCode);
        }
    }
}