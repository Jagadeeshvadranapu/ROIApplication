using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ROIApplication.Application;
using ROIApplication.Model;
using System.ComponentModel.DataAnnotations;

namespace ROIApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentServices investmentServices;
       // private readonly InvestmentValidator investmentValidator;

        public InvestmentController(IInvestmentServices investmentServices)
        {
            this.investmentServices = investmentServices;
           // this.investmentValidator = investmentValidator;
        }

        [HttpGet("GetInvestmentOptions")]
        public ActionResult<List<Model.InvestmentOption>> GetInvestmentOptions() {
            try
            {
                return Ok(this.investmentServices.GetInvestmentOptions());
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpGet("GetExchangeConvert")]
        public async Task<IActionResult> GetExchangeConvert(int amount, string from, string to)
        {
            try {
                
                double convertedRate = await this.investmentServices.GetExchangeConvert(amount, from, to);
                return Ok(convertedRate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("CalucateProjectedROI")]
        public async Task<IActionResult> CalucateProjectedROI([FromBody] Model.Investments Investments) {
            try {
                 ProjectROI projectROI =  await this.investmentServices.CalucateProjectedROI(Investments);
                return Ok(projectROI);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
