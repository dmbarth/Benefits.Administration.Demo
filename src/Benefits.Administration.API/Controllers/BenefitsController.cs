using Benefits.Administration.Application.Entities;
using Benefits.Administration.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Benefits.Administration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenefitsController : ControllerBase
    {
        private readonly ILogger<BenefitsController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public BenefitsController(ILogger<BenefitsController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Description("Get Benefits")]
        [ProducesResponseType(typeof(IEnumerable<Benefit>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBenefits([FromQuery] int? year)
        {
            if (year.HasValue)
            {
                var benefitsByYear = await _unitOfWork.Benefits.FindAsync(entity => entity.Year == year);

                return Ok(benefitsByYear);
            }

            var benefits = await _unitOfWork.Benefits.GetAllAsync();

            return Ok(benefits);
        }

        [HttpGet("{id}")]
        [Description("Get Benefits By Id")]
        [ProducesResponseType(typeof(Benefit), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBenefitById([FromRoute] long id)
        {
            var benefit = await _unitOfWork.Benefits.GetByIdAsync(id);

            return Ok(benefit);
        }
    }
}
