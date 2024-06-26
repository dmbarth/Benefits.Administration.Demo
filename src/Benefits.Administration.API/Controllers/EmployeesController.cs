using Benefits.Administration.Application.Entities;
using Benefits.Administration.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Benefits.Administration.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmployeesController : ControllerBase
  {
    private readonly ILogger<EmployeesController> _logger;
    private readonly IEmployeeService _employeeService;

    public EmployeesController(ILogger<EmployeesController> logger, IEmployeeService employeeService)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
    }

    [HttpGet]
    [Description("Get All Employees")]
    [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
      var employees = await _employeeService.GetEmployeesAsync();

      return Ok(employees);
    }

    [HttpGet("{employeeId}")]
    [Description("Get Employee")]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromRoute] long employeeId)
    {
      var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);

      return Ok(employee);
    }

    [HttpPost]
    [Description("Add Employee")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] Employee model)
    {
      var employeeId = await _employeeService.AddEmployeeAsync(model);

      return Created($"{Request.Path.Value}/{employeeId}", null);
    }

    [HttpPut("{employeeId}")]
    [Description("Update Employee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync([FromRoute] long employeeId, [FromBody] Employee model)
    {
      await _employeeService.UpdateEmployeeAsync(employeeId, model);

      return NoContent();
    }

    [HttpDelete("{employeeId}")]
    [Description("Delete Employee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] long employeeId)
    {
      await _employeeService.DeleteEmployeeAsync(employeeId);

      return NoContent();
    }
  }
}
