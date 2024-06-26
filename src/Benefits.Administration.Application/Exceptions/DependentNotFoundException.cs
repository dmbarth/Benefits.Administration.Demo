using System;

namespace Benefits.Administration.Application.Exceptions
{
  public class DependentNotFoundException : NotFoundException
  {
    public DependentNotFoundException() : base() { }
    public DependentNotFoundException(string message) : base(message) { }
    public DependentNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    public override string Text => "Dependent not found";
  }
}