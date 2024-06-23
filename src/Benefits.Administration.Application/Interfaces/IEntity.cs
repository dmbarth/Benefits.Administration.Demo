namespace Benefits.Administration.Application.Interfaces
{
  public interface IEntity<TId>
  {
    TId Id { get; }
  }
}
