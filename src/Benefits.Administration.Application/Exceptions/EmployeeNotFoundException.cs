using System;

namespace Benefits.Administration.Application.Exceptions
{
  public class EmployeeNotFoundException : NotFoundException
  {
    public EmployeeNotFoundException() : base() { }
    public EmployeeNotFoundException(string message) : base(message) { }
    public EmployeeNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    public override string Text => "Employee not found";
  }
}