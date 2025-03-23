namespace Domain.Entities.Base;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; }
    protected Entity(T id)
    {
        Id = id;
    }

    // protected Entity(Entity<T> entity)
    // {
    //     Id = entity.Id;
    // }
}
