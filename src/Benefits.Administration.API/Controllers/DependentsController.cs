using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Benefits.Administration.Application.Entities;
using Benefits.Administration.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Benefits.Administration.API.Controllers
{
  [Route("api/Employees/{employeeId}/[controller]")]
  [ApiController]
  public class DependentsController : ControllerBase
  {
    private readonly ILogger<DependentsController> _logger;
    private readonly IDependentsService _dependentsService;

    public DependentsController(ILogger<DependentsController> logger, IDependentsService dependentsService)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _dependentsService = dependentsService ?? throw new ArgumentNullException(nameof(dependentsService));
    }

    [HttpGet("{dependentId}")]
    [Description("Get Employee Dependent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromRoute] long employeeId, [FromRoute] long dependentId)
    {
      var dependent = await _dependentsService.GetDependentAsync(employeeId, dependentId);

      return Ok(dependent);
    }

    [HttpPost]
    [Description("Add Employee Dependent")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAsync([FromRoute] long employeeId, [FromBody] Dependent dependent)
    {
      var dependentId = await _dependentsService.AddDependentAsync(employeeId, dependent);

      return Created($"{Request.Path.Value}/{dependentId}", null);
    }

    [HttpPut("{dependentId}")]
    [Description("Update Employee Dependent")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync([FromRoute] long employeeId, [FromRoute] long dependentId, [FromBody] Dependent dependent)
    {
      await _dependentsService.UpdateDependentAsync(employeeId, dependentId, dependent);

      return NoContent();
    }

    [HttpDelete("{dependentId}")]
    [Description("Delete Employee Dependent")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] long employeeId, [FromRoute] long dependentId)
    {
      await _dependentsService.DeleteDependentAsync(employeeId, dependentId);

      return NoContent();
    }
  }
}