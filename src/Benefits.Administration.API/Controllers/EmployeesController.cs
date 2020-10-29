using AutoMapper;
using Benefits.Administration.Application.Entities;
using Benefits.Administration.Application.Interfaces;
using Benefits.Administration.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Benefits.Administration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICalculateAnnualDeductionsUseCase _useCase;

        public EmployeesController(ILogger<EmployeesController> logger, IUnitOfWork unitOfWork, IMapper mapper, ICalculateAnnualDeductionsUseCase useCase)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _useCase = useCase;
        }

        [HttpGet]
        [Description("Get Employees")]
        [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();

            return Ok(employees);
        }

        [HttpPost]
        [Description("Add Employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            _unitOfWork.Employees.Add(employee);

            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpGet("{id}")]
        [Description("Get Employee By Id")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployeeById([FromRoute] long id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpDelete("{id}")]
        [Description("Delete Employee By Id")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployeeById([FromRoute] long id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            _unitOfWork.Employees.Remove(employee);

            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        [Description("Update Employee By Id")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEmployeeById([FromRoute] long id, [FromBody] Employee employee)
        {
            var employeeEntity = await _unitOfWork.Employees.GetByIdAsync(id);

            if (employeeEntity == null)
                return NotFound();

            employeeEntity.FirstName = employee.FirstName;
            employeeEntity.MiddleName = employee.MiddleName;
            employeeEntity.LastName = employee.LastName;
            employeeEntity.Income = employee.Income;

            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpPost("{id}/Dependents")]
        [Description("Add Employee Dependent")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddEmployeeDependent([FromRoute] long id, [FromBody] Dependent dependent)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            employee.Dependents.Add(dependent);

            await _unitOfWork.CompleteAsync();

            return Ok(employee);
        }

        [HttpDelete("{id}/Dependents/{dependentId}")]
        [Description("Delete Employee Dependent")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployeeDependent([FromRoute] long id, [FromRoute] long dependentId)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            var dependent = employee.Dependents.Where(dep => dep.ID == dependentId).FirstOrDefault();

            if (dependent == null)
                return NotFound();

            employee.Dependents.Remove(dependent);

            await _unitOfWork.CompleteAsync();

            return Ok(employee);
        }

        [HttpPut("{id}/Dependents/{dependentId}")]
        [Description("Update Employee Dependent")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEmployeeDependent([FromRoute] long id, [FromRoute] long dependentId, [FromBody] Dependent dependent)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            var dep = employee.Dependents.Where(dep => dep.ID == dependentId).FirstOrDefault();

            if (dependent == null)
                return NotFound();

            dep.FirstName = dependent.FirstName;
            dep.MiddleName = dependent.MiddleName;
            dep.LastName = dependent.LastName;
            dep.Type = dependent.Type;

            await _unitOfWork.CompleteAsync();

            return Ok(employee);
        }

        [HttpGet("{id}/Deductions")]
        [Description("Get Employee Deductions")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployeeDeductions([FromRoute] long id, [FromQuery] int? costPeriods)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            var deductions = await _useCase.InvokeAsync(id, costPeriods);

            return Ok(deductions);
        }
    }
}
