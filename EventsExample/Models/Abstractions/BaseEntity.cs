using EventsExample.Interfaces;

namespace EventsExample.Models.Abstractions;

public abstract class BaseEntity: IEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    public User Author { get; set; }
}