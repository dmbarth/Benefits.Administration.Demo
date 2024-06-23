using Benefits.Administration.Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benefits.Administration.Application.Entities
{
  public abstract class Entity<TId> : IEntity<TId>
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TId Id { get; protected internal set; }
  }
}
