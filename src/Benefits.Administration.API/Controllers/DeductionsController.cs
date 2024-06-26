using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Benefits.Administration.Application.Interfaces.Services;
using Benefits.Administration.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Benefits.Administration.API.Controllers
{
  [Route("api/Employees/{employeeId}/[controller]")]
  [ApiController]
  public class DeductionsController : ControllerBase
  {
    private readonly ILogger<DeductionsController> _logger;
    private readonly IDeductionsService _deductionsService;

    public DeductionsController(ILogger<DeductionsController> logger, IDeductionsService deductionsService)
    {      
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _deductionsService = deductionsService ?? throw new ArgumentNullException(nameof(deductionsService));
    }

    [HttpGet]
    [Description("Get Deductions")]
    [ProducesResponseType(typeof(EmployeeDeductions), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromRoute] long employeeId, [FromQuery] int? costPeriods)
    {
      var deductions = await _deductionsService.CalculateDeductionsAsync(employeeId, costPeriods);

      return Ok(deductions);
    }
  }
}