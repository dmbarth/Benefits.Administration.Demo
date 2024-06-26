using System;

namespace Benefits.Administration.Application.Exceptions
{
  public abstract class NotFoundException : Exception
  {
    public NotFoundException() : base() { }
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception innerException) : base(message, innerException) { }

    public abstract string Text { get; }
  }
}