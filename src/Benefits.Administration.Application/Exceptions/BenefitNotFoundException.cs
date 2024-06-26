using System;

namespace Benefits.Administration.Application.Exceptions
{
  public class BenefitNotFoundException : NotFoundException
  {
    public BenefitNotFoundException() : base() { }
    public BenefitNotFoundException(string message) : base(message) { }
    public BenefitNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    public override string Text => "Benefit not found";
  }
}