using Benefits.Administration.Application.Entities;
using Benefits.Administration.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    private readonly IBenefitsService _benefitsService;

    public BenefitsController(ILogger<BenefitsController> logger, IBenefitsService benefitsService)
    {
      _logger = logger;
      _benefitsService = benefitsService;
    }

    [HttpGet]
    [Description("Get Benefits")]
    [ProducesResponseType(typeof(IEnumerable<Benefit>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBenefits([FromQuery] int? year)
    {
      var benefits = await _benefitsService.GetBenefitsAsync(year);

      return Ok(benefits);
    }

    [HttpGet("{id}")]
    [Description("Get Benefits By Id")]
    [ProducesResponseType(typeof(Benefit), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBenefitById([FromRoute] long id)
    {
      var benefit = await _benefitsService.GetBenefitByIdAsync(id);

      return Ok(benefit);
    }
  }
}
