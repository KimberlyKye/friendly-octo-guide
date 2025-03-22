namespace Domain.Defaults;

public abstract class Entity<T> : IEntity<T>
{
    private T _id;

    public T GetId()
    {
        return _id;
    }

    // protected Entity(T id)
    // {
    //     _id = id;
    // }

    // protected Entity(Entity<T> entity)
    // {
    //     _id = entity._id;
    // }
}
